using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Purchases
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            PurchaseModel = new();
            SupplierList = new();
        }
        public Pagination<PurchaseDto> PurchaseModel { get; set; }
        public List<string> SupplierList { get; set; }

        public async Task OnGetAsync(string searchText, string supplier, int pageIndex, int pageSize)
        {
            SupplierList = await unitOfWork.SupplierRepository.GetSupplierNamesAsync();
            PurchaseModel = await unitOfWork.PurchaseRepository.GetAllPurchasesAsync(searchText, supplier, pageIndex, pageSize);
        }
    }
}
