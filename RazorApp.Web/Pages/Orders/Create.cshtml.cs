using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateModel(IUnitOfWork unitOfWork)
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

        public async Task<IActionResult> OnGetAsync()
        {
            CustomerList = await unitOfWork.CustomerRepository.GetCustomerNamesAsync();
            ProductList = await unitOfWork.ProductRepository.GetProductsAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
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

                    CustomerList = await unitOfWork.CustomerRepository.GetCustomerNamesAsync();
                    ProductList = await unitOfWork.ProductRepository.GetProductsAsync();

                    return Page();
                }

                await unitOfWork.OrderRepository.CreateNewOrderAsync(OrderModel);

                await unitOfWork.ConfirmChangesAsync();

                return RedirectToPage("/Orders/Index");
            }
            else
            {
                CustomerList = await unitOfWork.CustomerRepository.GetCustomerNamesAsync();
                ProductList = await unitOfWork.ProductRepository.GetProductsAsync();

                return Page();
            }
        }

    }
}
