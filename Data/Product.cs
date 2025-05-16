using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest_Ev4.Data
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string Name { get; set; }

        public required string Sku { get; set; }

        public required int Price { get; set; }

        public required int Stock { get; set; }

        public required bool IsActive { get; set; } = true;
    }
}