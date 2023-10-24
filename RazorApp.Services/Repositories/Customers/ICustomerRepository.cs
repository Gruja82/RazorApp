using RazorApp.Data.Entities;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Repositories.Customers
{
    public interface ICustomerRepository:IGenericRepository<Customer>
    {
        Task<Pagination<CustomerDto>> GetAllCustomersAsync(string searchText, int pageIndex, int pageSize);
        Task<CustomerDto> GetSingleCustomerAsync(int id);
        Task CreateNewCustomerAsync(CustomerDto customerDto, string imagesFolder);
        Task UpdateCustomerAsync(CustomerDto customerDto, string imagesFolder);
        Task DeleteCustomerAsync(CustomerDto customerDto, string imagesFolder);
        Task<Dictionary<string, string>> ValidateCustomerAsync(CustomerDto customerDto);
        Task<List<string>> GetCustomerNamesAsync();
    }
}
