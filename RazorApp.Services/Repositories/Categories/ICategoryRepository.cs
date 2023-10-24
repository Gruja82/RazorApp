using RazorApp.Data.Entities;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Repositories.Categories
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Task<Pagination<CategoryDto>> GetAllCategoriesAsync(string searchText, int pageIndex, int pageSize);
        Task<CategoryDto> GetSingleCategoryAsync(int id);
        Task CreateNewCategoryAsync(CategoryDto categoryDto);
        Task UpdateCategoryAsync(CategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);
        Task<Dictionary<string, string>> ValidateCategoryAsync(CategoryDto categoryDto);
        Task<List<string>> GetCategoryNamesAsync();
    }
}
