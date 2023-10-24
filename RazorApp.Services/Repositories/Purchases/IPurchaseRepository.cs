using RazorApp.Data.Entities;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Repositories.Purchases
{
    public interface IPurchaseRepository:IGenericRepository<Purchase>
    {
        Task<Pagination<PurchaseDto>> GetAllPurchasesAsync(string searchText, string supplier, int pageIndex, int pageSize);
        Task<PurchaseDto> GetSinglePurchaseAsync(int id);
        Task CreateNewPurchaseAsync(PurchaseDto purchaseDto);
        Task UpdatePurchaseAsync(PurchaseDto purchaseDto);
        Task DeletePurchaseAsync(int id);
        Task<Dictionary<string, string>> ValidatePurchaseAsync(PurchaseDto purchaseDto);
    }
}
