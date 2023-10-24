using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Models.Dtos
{
    public class PurchaseDto:BaseDto
    {
        [Required]
        public string PurchaseCode { get; set; } = default!;
        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public string SupplierName { get; set; } = default!;
        public List<PurchaseDetailDto> PurchaseDetailDtos { get; set; } = new();
    }
}
