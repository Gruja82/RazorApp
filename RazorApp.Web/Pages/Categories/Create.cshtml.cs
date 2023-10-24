using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.Categories;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        public CreateModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            CategoryModel = new();
        }

        [BindProperty]
        public CategoryDto CategoryModel { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
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

                await unitOfWork.CategoryRepository.CreateNewCategoryAsync(CategoryModel);

                await unitOfWork.ConfirmChangesAsync();

                return RedirectToPage("/Categories/Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
