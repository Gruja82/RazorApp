using RazorApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Utilities
{
    public static class PaginationUtility<T> where T : class
    {
        public static Pagination<T> GetPaginatedResult(in List<T> dataList, int currentPage, int pageSize)
        {
            var pagination = new Pagination<T>();

            pagination.DataSet = (from c in dataList select c)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            double pageCount = (double)dataList.Count / pageSize;

            pagination.TotalPages = (int)Math.Ceiling(pageCount);

            pagination.PageIndex = currentPage;

            pagination.PageSize = pageSize;

            return pagination;
        }
    }
}
