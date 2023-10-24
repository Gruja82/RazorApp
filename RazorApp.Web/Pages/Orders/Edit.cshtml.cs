using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            OrderModel = new();
            CustomerList = new();
            ProductList = new();
        }
        [BindProperty]
        public OrderDto OrderModel { get; set; }
        public List<string> CustomerList { get; set; }
        public List<ProductDto> ProductList { get; set; }

        public async Task OnGetAsync(int id)
        {
            CustomerList = await unitOfWork.CustomerRepository.GetCustomerNamesAsync();
            ProductList = await unitOfWork.ProductRepository.GetProductsAsync();
            OrderModel = await unitOfWork.OrderRepository.GetSingleOrderAsync(id);
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (ModelState.IsValid)
            {
                var errors = await unitOfWork.OrderRepository.ValidateOrderAsync(OrderModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError("OrderModel." + error.Key, error.Value);
                    }

                    return Page();
                }

                await unitOfWork.OrderRepository.UpdateOrderAsync(OrderModel);

                await unitOfWork.ConfirmChangesAsync();

                return RedirectToPage("/Orders/Index");
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            await unitOfWork.OrderRepository.DeleteOrderAsync(OrderModel.Id);

            await unitOfWork.ConfirmChangesAsync();

            return RedirectToPage("/Orders/Index");
        }
    }
}
