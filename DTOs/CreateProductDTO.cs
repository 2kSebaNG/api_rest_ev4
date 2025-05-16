using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest_Ev4.DTOs
{
    public class CreateProductDTO
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, ErrorMessage = "El nombre del producto no puede exceder los 50 caracteres")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "El SKU es requerido")]
        [StringLength(30, ErrorMessage = "El SKU no puede exceder los 30 caracteres")]
        public required string Sku { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El precio debe ser mayor que cero")]
        public required int Price { get; set; }

        [Required(ErrorMessage = "El stock es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El stock debe ser mayor que cero")]
        public required int Stock { get; set; }
    }
}