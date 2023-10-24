using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Models.Dtos
{
    public class PurchaseDetailDto
    {
        public int Id { get; set; }
        public string PurchaseCode { get; set; } = default!;
        public string MaterialName { get; set; } = default!;
        public double Qty { get; set; }
    }
}
