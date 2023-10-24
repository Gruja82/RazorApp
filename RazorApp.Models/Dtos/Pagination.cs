using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Models.Dtos
{
    public class Pagination<T> where T : class
    {
        public List<T> DataSet { get; set; } = new();
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
