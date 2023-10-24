using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            ProductModel = new();
            CategoryList = new();
        }

        public Pagination<ProductDto> ProductModel { get; set; }
        public List<string> CategoryList { get; set; }

        public async Task OnGetAsync(string searchText, string category, int pageIndex, int pageSize)
        {
            CategoryList = await unitOfWork.CategoryRepository.GetCategoryNamesAsync();

            ProductModel = await unitOfWork.ProductRepository.GetAllProductsAsync(searchText, category, pageIndex, pageSize);
        }
    }
}
