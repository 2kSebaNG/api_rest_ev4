using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest_Ev4.Data;
using ApiRest_Ev4.DTOs;

namespace ApiRest_Ev4.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddProduct(Product product);

        Task<Product?> GetProductById(Guid id);

        Task<Product[]?> GetAllProducts(ProductQueryDTO productQuery);

        Task<Product?> UpdateProduct(Guid id, UpdateProductDTO updateProduct);

        Task<Product?> DeleteProduct(Guid id);
    }
}