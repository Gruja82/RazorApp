using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            OrderModel = new();
            CustomerList = new();
        }
        public Pagination<OrderDto> OrderModel { get; set; }
        public List<string> CustomerList { get; set; }

        public async Task OnGetAsync(string searchText, string customer, int pageIndex, int pageSize)
        {
            CustomerList = await unitOfWork.CustomerRepository.GetCustomerNamesAsync();
            OrderModel = await unitOfWork.OrderRepository.GetAllOrdersAsync(searchText, customer, pageIndex, pageSize);
        }
    }
}
