using RazorApp.Data.Entities;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Repositories.Suppliers
{
    public interface ISupplierRepository:IGenericRepository<Supplier>
    {
        Task<Pagination<SupplierDto>> GetAllSuppliersAsync(string searchText, int pageIndex, int pageSize);
        Task<SupplierDto> GetSingleSupplierAsync(int id);
        Task CreateNewSupplierAsync(SupplierDto supplierDto, string imagesFolder);
        Task UpdateSupplierAsync(SupplierDto supplierDto, string imagesFolder);
        Task DeleteSupplierAsync(SupplierDto supplierDto, string imagesFolder);
        Task<Dictionary<string,string>> ValidateSupplierAsync(SupplierDto supplierDto);
        Task<List<string>> GetSupplierNamesAsync();
    }
}
