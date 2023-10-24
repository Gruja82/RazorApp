using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EditModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
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

        public async Task OnGetAsync(int id)
        {
            CategoryList = await unitOfWork.CategoryRepository.GetCategoryNamesAsync();
            MaterialList = await unitOfWork.MaterialRepository.GetMaterialDtos();

            ProductModel = await unitOfWork.ProductRepository.GetSingleProductAsync(id);
        }

        public async Task<IActionResult> OnPostEditAsync()
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

                await unitOfWork.ProductRepository.UpdateProductAsync(ProductModel, imagesFolder);

                await unitOfWork.ConfirmChangesAsync();

                return RedirectToPage("/Products/Index");
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            string imagesFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/Products");

            await unitOfWork.ProductRepository.DeleteProductAsync(ProductModel, imagesFolder);

            await unitOfWork.ConfirmChangesAsync();

            return RedirectToPage("/Products/Index");
        }
    }
}
