using RazorApp.Data.AbstractEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Data.Entities
{
    public class OrderDetail:IdEntity
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Qty { get; set; }
        public virtual Order Order { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;
    }
}
