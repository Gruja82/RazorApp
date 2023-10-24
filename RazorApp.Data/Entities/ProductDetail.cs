using RazorApp.Data.AbstractEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Data.Entities
{
    public class ProductDetail:IdEntity
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int MaterialId { get; set; }
        [Required]
        public double Qty { get; set; }
        public virtual Product Product { get; set; } = default!;
        public virtual Material Material { get; set; } = default!;
    }
}
