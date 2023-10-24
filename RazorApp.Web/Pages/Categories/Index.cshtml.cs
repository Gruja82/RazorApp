using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            CategoryModel = new();
        }

        public Pagination<CategoryDto> CategoryModel { get; set; }

        public async Task OnGetAsync(string searchText, int pageIndex, int pageSize)
        {
            CategoryModel = await unitOfWork.CategoryRepository.GetAllCategoriesAsync(searchText, pageIndex, pageSize);
        }
    }
}
