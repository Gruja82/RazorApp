using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Models.Dtos
{
    public class OrderDto:BaseDto
    {
        [Required]
        public string OrderCode { get; set; } = default!;
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string CustomerName { get; set; } = default!;
        public List<OrderDetailDto> OrderDetailDtos { get; set; } = new();
    }
}
