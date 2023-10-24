using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Materials
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            MaterialModel = new();
            CategoryList = new();
        }

        public Pagination<MaterialDto> MaterialModel { get; set; }

        public List<string> CategoryList { get; set; }

        public async Task OnGetAsync(string searchText, string category, int pageIndex, int pageSize)
        {
            CategoryList = await unitOfWork.CategoryRepository.GetCategoryNamesAsync();

            MaterialModel = await unitOfWork.MaterialRepository.GetAllMaterialsAsync(searchText, category, pageIndex, pageSize);
        }
    }
}
