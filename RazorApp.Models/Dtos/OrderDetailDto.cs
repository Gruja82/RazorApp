using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Models.Dtos
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public string OrderCode { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public int Qty { get; set; }
    }
}
