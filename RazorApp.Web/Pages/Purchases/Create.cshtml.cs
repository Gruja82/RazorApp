using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Purchases
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            PurchaseModel = new();
            SupplierList = new();
            MaterialList = new();
        }

        [BindProperty]
        public PurchaseDto PurchaseModel { get; set; }
        public List<string> SupplierList { get; set; }
        public List<MaterialDto> MaterialList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            SupplierList = await unitOfWork.SupplierRepository.GetSupplierNamesAsync();
            MaterialList = await unitOfWork.MaterialRepository.GetMaterialDtos();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var errors = await unitOfWork.PurchaseRepository.ValidatePurchaseAsync(PurchaseModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError("PurchaseModel." + error.Key, error.Value);
                    }

                    SupplierList = await unitOfWork.SupplierRepository.GetSupplierNamesAsync();
                    MaterialList = await unitOfWork.MaterialRepository.GetMaterialDtos();

                    return Page();
                }

                await unitOfWork.PurchaseRepository.CreateNewPurchaseAsync(PurchaseModel);

                await unitOfWork.ConfirmChangesAsync();

                return RedirectToPage("/Purchases/Index");
            }
            else
            {
                SupplierList = await unitOfWork.SupplierRepository.GetSupplierNamesAsync();
                MaterialList = await unitOfWork.MaterialRepository.GetMaterialDtos();

                return Page();
            }
        }
    }
}
