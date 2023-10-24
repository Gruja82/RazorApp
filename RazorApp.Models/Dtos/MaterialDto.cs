using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Models.Dtos
{
    public class MaterialDto:BaseDto
    {
        [Required]
        public string Code { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public string CategoryName { get; set; } = default!;
        [Required]
        public double Qty { get; set; }
        [Required]
        public double Price { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
    }
}
