using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest_Ev4.Data;
using ApiRest_Ev4.DTOs;
using ApiRest_Ev4.Repositories;

namespace ApiRest_Ev4.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Product> AddProduct(CreateProductDTO createProduct)
        {
            var product = new Product
            {
                Name = createProduct.Name,
                Sku = createProduct.Sku,
                Price = createProduct.Price,
                Stock = createProduct.Stock,
                IsActive = true,
            };

            return _productRepository.AddProduct(product) ?? throw new Exception("Error al agregar el producto");
        }

        public async Task<Product?> DeleteProduct(string id)
        {
            return await _productRepository.DeleteProduct(Guid.Parse(id));
        }

        public async Task<Product[]?> GetAllProducts(ProductQueryDTO productQuery)
        {
            var products = await _productRepository.GetAllProducts(productQuery);

            if (products == null || !products.Any())
            {
                return null;
            }

            return products.ToArray();
        }

        public async Task<Product?> GetProductById(string id)
        {
            var product = await _productRepository.GetProductById(Guid.Parse(id));

            if (product == null)
            {
                return null;
            }

            return product;
        }

        public async Task<Product> UpdateProduct(string id, UpdateProductDTO updateProduct)
        {
            
            
            return await _productRepository.UpdateProduct(Guid.Parse(id), updateProduct) ?? throw new Exception("Error al actualizar el producto");
        }
    }
}