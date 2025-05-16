using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest_Ev4.Data;
using ApiRest_Ev4.DTOs;

namespace ApiRest_Ev4.Services
{
    public interface IProductService
    {
        Task<Product> AddProduct(CreateProductDTO createProduct);

        Task<Product> UpdateProduct(string id, UpdateProductDTO updateProduct);

        Task<Product?> GetProductById(string id);
        
        Task<Product[]?> GetAllProducts(ProductQueryDTO productQuery);

        Task<Product?> DeleteProduct(string id);
    }
}