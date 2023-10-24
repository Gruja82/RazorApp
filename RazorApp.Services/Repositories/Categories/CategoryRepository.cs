using Microsoft.EntityFrameworkCore;
using RazorApp.Data.Database;
using RazorApp.Data.Entities;
using RazorApp.Models.Dtos;
using RazorApp.Services.Extensions;
using RazorApp.Services.Repositories.Generic;
using RazorApp.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Repositories.Categories
{
    public class CategoryRepository:GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context):base(context)
        {
            
        }

        public async Task CreateNewCategoryAsync(CategoryDto categoryDto)
        {
            Category category = new();

            category.Code = categoryDto.Code;
            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;

            await CreateNewRecordAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await DeleteRecordAsync(id);
        }

        public async Task<Pagination<CategoryDto>> GetAllCategoriesAsync(string searchText, int pageIndex, int pageSize)
        {
            var allCategories = await GetAllRecordsAsync();

            if (!string.IsNullOrEmpty(searchText))
            {
                allCategories = allCategories.Where(e => e.Code.ToLower().Contains(searchText.ToLower())
                                                    || e.Name.ToLower().Contains(searchText.ToLower()));
            }

            List<CategoryDto> categoryDtos = new();

            foreach (var category in allCategories)
            {
                categoryDtos.Add(category.ConvertToDto());
            }

            return PaginationUtility<CategoryDto>.GetPaginatedResult(in categoryDtos, pageIndex == 0 ? 1 : pageIndex, pageSize == 0 ? 4 : pageSize);
        }

        public async Task<CategoryDto> GetSingleCategoryAsync(int id)
        {
            Category category = await GetSingleRecordAsync(id);

            return category.ConvertToDto();
        }

        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            Category category = await GetSingleRecordAsync(categoryDto.Id);

            category.Code = categoryDto.Code;
            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;

            UpdateRecord(category);
        }

        public async Task<Dictionary<string, string>> ValidateCategoryAsync(CategoryDto categoryDto)
        {
            Dictionary<string, string> errors = new();

            var allCategories = await GetAllRecordsAsync();

            if (categoryDto.Id > 0)
            {
                Category category = await GetSingleRecordAsync(categoryDto.Id);

                if (category.Code != categoryDto.Code)
                {
                    if (allCategories.Select(e => e.Code.ToLower()).Contains(categoryDto.Code.ToLower()))
                    {
                        errors.Add("Code", "There is already Category with this code in database. Please provide different one!");
                    }
                }

                if (category.Name != categoryDto.Name)
                {
                    if (allCategories.Select(e => e.Name.ToLower()).Contains(categoryDto.Name.ToLower()))
                    {
                        errors.Add("Name", "There is already Category with this name in database. Please provide different one!");
                    }
                }
            }
            else
            {
                if (allCategories.Select(e => e.Code.ToLower()).Contains(categoryDto.Code.ToLower()))
                {
                    errors.Add("Code", "There is already Category with this code in database. Please provide different one!");
                }

                if (allCategories.Select(e => e.Name.ToLower()).Contains(categoryDto.Name.ToLower()))
                {
                    errors.Add("Name", "There is already Category with this name in database. Please provide different one!");
                }
            }

            return errors;
        }

        public async Task<List<string>> GetCategoryNamesAsync()
        {
            List<string> categoryList = await context.Categories.Select(e => e.Name).ToListAsync();

            return categoryList;
        }
    }
}
