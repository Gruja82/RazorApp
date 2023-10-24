using RazorApp.Data.AbstractEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Data.Entities
{
    public class Purchase:CodeEntity
    {
        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; } = default!;
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = default!;
    }
}
