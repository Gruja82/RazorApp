using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Productions
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            ProductionModel = new();
            ProductList = new();
        }
        [BindProperty]
        public ProductionDto ProductionModel { get; set; }
        public List<string> ProductList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = (await unitOfWork.ProductRepository.GetProductsAsync()).Select(e => e.Name).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var errors = await unitOfWork.ProductionRepository.ValidateProductionAsync(ProductionModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError("ProductionModel." + error.Key, error.Value);
                    }

                    ProductList = (await unitOfWork.ProductRepository.GetProductsAsync()).Select(e => e.Name).ToList();

                    return Page();
                }

                await unitOfWork.ProductionRepository.CreateNewProductionAsync(ProductionModel);

                await unitOfWork.ConfirmChangesAsync();

                return RedirectToPage("/Productions/Index");
            }
            else
            {
                ProductList = (await unitOfWork.ProductRepository.GetProductsAsync()).Select(e => e.Name).ToList();

                return Page();
            }
        }
    }
}
