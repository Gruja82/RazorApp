using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.UnitOfWork;

namespace RazorApp.Web.Pages.Productions
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            ProductionModel = new();
            ProductList = new();
            ProductionDates = new();
        }
        public Pagination<ProductionDto> ProductionModel { get; set; }
        public List<string> ProductList { get; set; }
        public List<DateTime> ProductionDates { get; set; }
        public async Task OnGetAsync(string searchText, DateTime productionDate, string product, int pageIndex, int pageSize)
        {
            ProductList = (await unitOfWork.ProductRepository.GetProductsAsync()).Select(e => e.Name).ToList();
            ProductionDates = await unitOfWork.ProductionRepository.GetProductionDatesAsync();
            ProductionModel = await unitOfWork.ProductionRepository.GetAllProductionsAsync(searchText, productionDate, product, pageIndex, pageSize);
        }
    }
}
