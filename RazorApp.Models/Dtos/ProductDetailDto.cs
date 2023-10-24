using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Models.Dtos
{
    public class ProductDetailDto
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = default!;

        public string MaterialName { get; set; } = default!;

        public double Qty { get; set; }
    }
}
