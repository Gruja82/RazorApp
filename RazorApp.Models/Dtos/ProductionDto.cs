using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Models.Dtos
{
    public class ProductionDto:BaseDto
    {
        [Required]
        public string Code { get; set; } = default!;
        [Required]
        public DateTime ProductionDate { get; set; }
        [Required]
        public int Qty { get; set; }
        [Required]
        public string ProductName { get; set; } = default!;

    }
}
