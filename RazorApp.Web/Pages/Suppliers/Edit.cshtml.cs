using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Suppliers
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EditModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
            SupplierModel = new();
        }
        [BindProperty]
        public SupplierDto SupplierModel { get; set; }

        public async Task OnGetAsync(int id)
        {
            SupplierModel = await unitOfWork.SupplierRepository.GetSingleSupplierAsync(id);
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if(ModelState.IsValid)
            {
                var errors = await unitOfWork.SupplierRepository.ValidateSupplierAsync(SupplierModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError("SupplierModel." + error.Key, error.Value);
                    }

                    return Page();
                }

                string imagesFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/Suppliers");

                await unitOfWork.SupplierRepository.UpdateSupplierAsync(SupplierModel, imagesFolder);

                await unitOfWork.ConfirmChangesAsync();

                return RedirectToPage("/Suppliers/Index");
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            string imagesFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/Suppliers");

            await unitOfWork.SupplierRepository.DeleteSupplierAsync(SupplierModel, imagesFolder);

            await unitOfWork.ConfirmChangesAsync();

            return RedirectToPage("/Suppliers/Index");
        }
    }
}
