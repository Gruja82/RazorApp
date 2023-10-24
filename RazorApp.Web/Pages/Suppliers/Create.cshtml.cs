using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Suppliers
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CreateModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
            SupplierModel = new();
        }

        [BindProperty]
        public SupplierDto SupplierModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
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

                await unitOfWork.SupplierRepository.CreateNewSupplierAsync(SupplierModel, imagesFolder);

                await unitOfWork.ConfirmChangesAsync();

                return RedirectToPage("/Suppliers/Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
