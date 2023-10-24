using RazorApp.Data.Entities;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Repositories.Productions
{
    public interface IProductionRepository:IGenericRepository<Production>
    {
        Task<Pagination<ProductionDto>> GetAllProductionsAsync(string searchText, DateTime productionDate, string product, int pageIndex, int pageSize);
        Task<ProductionDto> GetSingleProductionAsync(int id);
        Task CreateNewProductionAsync(ProductionDto productionDto);
        Task UpdateProductionAsync(ProductionDto productionDto);
        Task DeleteProductionAsync(int id);
        Task<Dictionary<string,string>> ValidateProductionAsync(ProductionDto productionDto);
        Task<List<DateTime>> GetProductionDatesAsync();
    }
}
