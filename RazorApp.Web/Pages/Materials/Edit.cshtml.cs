using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Materials
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EditModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
            MaterialModel = new();
            CategoryList = new();
        }

        [BindProperty]
        public MaterialDto MaterialModel { get; set; }

        public List<string> CategoryList { get; set; }

        public async Task OnGetAsync(int id)
        {
            CategoryList = await unitOfWork.CategoryRepository.GetCategoryNamesAsync();
            MaterialModel = await unitOfWork.MaterialRepository.GetSingleMaterialAsync(id);
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (ModelState.IsValid)
            {
                var errors = await unitOfWork.MaterialRepository.ValidateMaterialAsync(MaterialModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError("MaterialModel." + error.Key, error.Value);
                    }

                    return Page();
                }

                string imagesFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/Materials");

                await unitOfWork.MaterialRepository.UpdateMaterialAsync(MaterialModel, imagesFolder);

                await unitOfWork.ConfirmChangesAsync();

                return RedirectToPage("/Materials/Inbdex");
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            string imagesFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/Materials");

            await unitOfWork.MaterialRepository.DeleteMaterialAsync(MaterialModel, imagesFolder);

            await unitOfWork.ConfirmChangesAsync();

            return RedirectToPage("/Materials/Index");
        }
    }
}
