using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest_Ev4.DTOs
{
    public class UpdateProductDTO
    {
        [StringLength(50, ErrorMessage = "El nombre del producto no puede exceder los 50 caracteres")]
        public string? Name { get; set; }

        [StringLength(30, ErrorMessage = "El SKU no puede exceder los 30 caracteres")]
        public string? Sku { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El precio debe ser mayor que cero")]
        public int Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El stock debe ser mayor que cero")]
        public int Stock { get; set; }
    }
}