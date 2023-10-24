using RazorApp.Data.AbstractEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Data.Entities
{
    public class Order:CodeEntity
    {
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = default!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = default!;
    }
}
