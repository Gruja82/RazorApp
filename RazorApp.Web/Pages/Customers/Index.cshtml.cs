using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            CustomerModel = new();
        }

        public Pagination<CustomerDto> CustomerModel { get; set; }

        public async Task OnGetAsync(string searchText, int pageIndex, int pageSize)
        {
            CustomerModel = await unitOfWork.CustomerRepository.GetAllCustomersAsync(searchText, pageIndex, pageSize);
        }
    }
}
