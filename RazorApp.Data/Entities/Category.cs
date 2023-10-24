using RazorApp.Data.AbstractEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Data.Entities
{
    public class Category:NameEntity
    {
        public string? Description { get; set; }
    }
}
