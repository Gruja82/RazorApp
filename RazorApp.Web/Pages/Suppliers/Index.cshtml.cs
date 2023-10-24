using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Suppliers
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            SupplierModel = new();
        }
        public Pagination<SupplierDto> SupplierModel { get; set; }

        public async Task OnGetAsync(string searchText, int pageIndex, int pageSize)
        {
            SupplierModel = await unitOfWork.SupplierRepository.GetAllSuppliersAsync(searchText, pageIndex, pageSize);
        }
    }
}
