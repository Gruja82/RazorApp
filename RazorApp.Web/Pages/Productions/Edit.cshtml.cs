using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Productions
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            ProductionModel = new();
            ProductList = new();
        }
        [BindProperty]
        public ProductionDto ProductionModel { get; set; }
        public List<string> ProductList { get; set; }

        public async Task OnGetAsync(int id)
        {
            ProductList = (await unitOfWork.ProductRepository.GetProductsAsync()).Select(e => e.Name).ToList();
            ProductionModel = await unitOfWork.ProductionRepository.GetSingleProductionAsync(id);
        }

        public async Task<IActionResult> OnPostEditAsync()
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

                    return Page();
                }

                await unitOfWork.ProductionRepository.UpdateProductionAsync(ProductionModel);

                await unitOfWork.ConfirmChangesAsync();

                return RedirectToPage("/Productions/Index");
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            await unitOfWork.ProductionRepository.DeleteProductionAsync(ProductionModel.Id);

            await unitOfWork.ConfirmChangesAsync();

            return RedirectToPage("/Productions/Index");
        }
    }
}
