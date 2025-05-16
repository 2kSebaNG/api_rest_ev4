using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest_Ev4.Data;
using ApiRest_Ev4.DTOs;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using Sprache;

namespace ApiRest_Ev4.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProduct(Product product)
        {

            var existingProduct = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Sku == product.Sku);

            if(existingProduct != null)
            {
                throw new InvalidOperationException("Ya existe un producto con ese SKU");
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
               return null;
            }

            product.IsActive = false;

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product[]?> GetAllProducts(ProductQueryDTO productQuery)
        {
           
            return await _context.Products.Skip((productQuery.Page - 1) * productQuery.ProductLimit)
                .Take(productQuery.ProductLimit)
                .ToArrayAsync();
        }

        public async Task<Product?> GetProductById(Guid id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> UpdateProduct(Guid id, UpdateProductDTO updateProduct)
        {
            var product = await _context.Products.FindAsync(id) ?? throw new NullReferenceException("Producto no encontrado");

            if(product.IsActive == false){

                throw new InvalidOperationException("No se puede modificar un producto eliminado");
            }

            if(!string.IsNullOrWhiteSpace(updateProduct.Name) && updateProduct.Name != product.Name)
            {
                product.Name = updateProduct.Name;
            }

            if(!string.IsNullOrWhiteSpace(updateProduct.Sku) && updateProduct.Sku != product.Sku)
            {
                
                var existingProduct = await _context.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Sku == updateProduct.Sku);

                if(existingProduct != null)
                {
                    throw new InvalidOperationException("Ya existe un producto con ese SKU");
                }
                
                product.Sku = updateProduct.Sku;
            }

            if(updateProduct.Price != product.Price)
            {
                product.Price = updateProduct.Price;
            }

            if(updateProduct.Stock != product.Stock)
            {
                product.Stock = updateProduct.Stock;
            }

            await _context.SaveChangesAsync();

            return product;
        }
    }
}