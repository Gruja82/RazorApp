using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Data.AbstractEntities
{
    public abstract class CodeEntity:IdEntity
    {
        [Required]
        public string Code { get; set; } = default!;
    }
}
