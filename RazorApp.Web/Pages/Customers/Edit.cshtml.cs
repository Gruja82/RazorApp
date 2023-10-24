using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.Customers;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EditModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
            CustomerModel = new();
        }

        [BindProperty]
        public CustomerDto CustomerModel { get; set; }

        public async Task OnGetAsync(int id)
        {
            CustomerModel = await unitOfWork.CustomerRepository.GetSingleCustomerAsync(id);
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (ModelState.IsValid)
            {
                var errors = await unitOfWork.CustomerRepository.ValidateCustomerAsync(CustomerModel);

                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        ModelState.AddModelError("CustomerModel." + error.Key, error.Value);
                    }

                    return Page();
                }

                string imagesFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/Customers");

                await unitOfWork.CustomerRepository.UpdateCustomerAsync(CustomerModel, imagesFolder);

                await unitOfWork.ConfirmChangesAsync();

                return RedirectToPage("/Customers/Index");
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            string imagesFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/Customers");

            await unitOfWork.CustomerRepository.DeleteCustomerAsync(CustomerModel, imagesFolder);

            await unitOfWork.ConfirmChangesAsync();

            return RedirectToPage("/Customers/Index");
        }
    }
}
