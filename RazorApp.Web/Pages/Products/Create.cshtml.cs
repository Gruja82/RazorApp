using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CreateModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
            ProductModel = new();
            CategoryList = new();
            MaterialList = new();
        }
        [BindProperty]
        public ProductDto ProductModel { get; set; }
        public List<string> CategoryList { get; set; }
        public List<MaterialDto> MaterialList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CategoryList = await unitOfWork.CategoryRepository.GetCategoryNamesAsync();
            MaterialList = await unitOfWork.MaterialRepository.GetMaterialDtos();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var errors = await unitOfWork.ProductRepository.ValidateProductAsync(ProductModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError("ProductModel." + error.Key, error.Value);
                    }

                    return Page();
                }

                string imagesFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/Products");

                await unitOfWork.ProductRepository.CreateNewProductAsync(ProductModel, imagesFolder);

                await unitOfWork.ConfirmChangesAsync();

                return RedirectToPage("/Products/Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
