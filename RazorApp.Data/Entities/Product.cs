using RazorApp.Data.AbstractEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Data.Entities
{
    public class Product:NameEntity
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int Qty { get; set; }
        [Required]
        public double Price { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
        public virtual Category Category { get; set; } = default!;
        public virtual ICollection<ProductDetail> ProductDetails { get; set; } = default!;
        public virtual ICollection<Production> Productions { get; set; } = default!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = default!;
    }
}
