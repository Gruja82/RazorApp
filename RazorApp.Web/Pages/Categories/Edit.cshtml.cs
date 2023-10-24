using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.Categories;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        public EditModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            CategoryModel = new();
        }

        [BindProperty]
        public CategoryDto CategoryModel { get; set; }

        public async Task OnGetAsync(int id)
        {
            CategoryModel = await unitOfWork.CategoryRepository.GetSingleCategoryAsync(id);
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (ModelState.IsValid)
            {
                var errors = await unitOfWork.CategoryRepository.ValidateCategoryAsync(CategoryModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError("CategoryModel." + error.Key, error.Value);
                    }

                    return Page();
                }

                await unitOfWork.CategoryRepository.UpdateCategoryAsync(CategoryModel);

                await unitOfWork.ConfirmChangesAsync();

                return RedirectToPage("/Categories/Index");
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            await unitOfWork.CategoryRepository.DeleteCategoryAsync(CategoryModel.Id);

            await unitOfWork.ConfirmChangesAsync();

            return RedirectToPage("/Categories/Index");
        }
    }
}
