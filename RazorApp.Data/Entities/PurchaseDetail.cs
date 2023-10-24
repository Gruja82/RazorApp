using RazorApp.Data.AbstractEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Data.Entities
{
    public class PurchaseDetail:IdEntity
    {
        [Required]
        public int PurchaseId { get; set; }
        [Required]
        public int MaterialId { get; set; }
        [Required]
        public double Qty { get; set; }
        public virtual Purchase Purchase { get; set; } = default!;
        public virtual Material Material { get; set; } = default!;
    }
}
