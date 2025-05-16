using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest_Ev4.DTOs
{
    public class ProductQueryDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "La página debe ser mayor que cero")]
        public int Page { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El límite de productos debe ser mayor que cero")]
        public int ProductLimit { get; set; }
    }
}