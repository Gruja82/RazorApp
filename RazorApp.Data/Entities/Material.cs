using RazorApp.Data.AbstractEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Data.Entities
{
    public class Material:NameEntity
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public double Qty { get; set; }
        [Required]
        public double Price { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
        public virtual Category Category { get; set; } = default!;
        public virtual ICollection<ProductDetail> ProductDetails { get; set; } = default!;
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = default!;
    }
}
