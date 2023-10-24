using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Data.AbstractEntities
{
    public abstract class NameEntity:CodeEntity
    {
        [Required]
        public string Name { get; set; } = default!;
    }
}
