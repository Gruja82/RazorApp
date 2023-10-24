using RazorApp.Data.Entities;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Repositories.Products
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<Pagination<ProductDto>> GetAllProductsAsync(string searchText, string category, int pageIndex, int pageSize);
        Task<ProductDto> GetSingleProductAsync(int id);
        Task CreateNewProductAsync(ProductDto productDto, string imagesFolder);
        Task UpdateProductAsync(ProductDto productDto, string imagesFolder);
        Task DeleteProductAsync(ProductDto productDto, string imagesFolder);
        Task<Dictionary<string, string>> ValidateProductAsync(ProductDto productDto);
        Task<List<ProductDto>> GetProductsAsync();
    }
}
