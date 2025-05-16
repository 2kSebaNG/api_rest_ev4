using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRest_Ev4.DTOs;
using ApiRest_Ev4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest_Ev4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDTO createProduct)
        {
            
            try
            {
                var product = await _productService.AddProduct(createProduct);
                return Created($"api/Product/{product.Id}", product);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetProductById(string id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                if (product == null)
                {
                    return NotFound("Producto no encontrado");
                }
                return Ok(product);
            }
            catch (FormatException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Guid no reconocido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryDTO productQuery)
        {
            try
            {
                var products = await _productService.GetAllProducts(productQuery);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] UpdateProductDTO updateProduct)
        {
            try
            {
                var product = await _productService.UpdateProduct(id, updateProduct);
                return Ok(product);
            }
            catch (FormatException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Guid no reconocido");
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                var product = await _productService.DeleteProduct(id);
                
                if (product == null)
                {
                    return NotFound("Producto no encontrado");
                }
                return NoContent();
            }
            catch (FormatException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Guid no reconocido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}