using RazorApp.Data.AbstractEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Data.Entities
{
    public class Supplier:NameEntity
    {
        [Required]
        public string Contact { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Postal { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string? Web { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public virtual ICollection<Order> Orders { get; set; } = default!;

    }
}
