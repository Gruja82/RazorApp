using Microsoft.EntityFrameworkCore;
using RazorApp.Data.Database;
using RazorApp.Data.Entities;
using RazorApp.Models.Dtos;
using RazorApp.Services.Extensions;
using RazorApp.Services.Repositories.Generic;
using RazorApp.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Repositories.Customers
{
    public class CustomerRepository:GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context):base(context)
        {
            
        }

        public async Task CreateNewCustomerAsync(CustomerDto customerDto, string imagesFolder)
        {
            Customer customer = new();

            customer.Code = customerDto.Code;
            customer.Name = customerDto.Name;
            customer.Contact = customerDto.Contact;
            customer.Address = customerDto.Address;
            customer.City = customerDto.City;
            customer.Postal = customerDto.Postal;
            customer.Phone = customerDto.Phone;
            customer.Email = customerDto.Email;
            customer.Web = customerDto.Web;
            customer.ImageUrl = customerDto.StoreImage(imagesFolder);

            await CreateNewRecordAsync(customer);
        }

        public async Task DeleteCustomerAsync(CustomerDto customerDto, string imagesFolder)
        {
            Customer customer = await GetSingleRecordAsync(customerDto.Id);

            await DeleteRecordAsync(customerDto.Id);

            if (!string.IsNullOrEmpty(customerDto.ImageUrl))
            {
                if(System.IO.File.Exists(Path.Combine(imagesFolder, customer.ImageUrl!)))
                {
                    System.IO.File.Delete(Path.Combine(imagesFolder, customer.ImageUrl!));
                }
            }
        }

        public async Task<Pagination<CustomerDto>> GetAllCustomersAsync(string searchText, int pageIndex, int pageSize)
        {
            var allCustomers = await GetAllRecordsAsync();

            if (!string.IsNullOrEmpty(searchText))
            {
                allCustomers = allCustomers.Where(e => e.Code.ToLower().Contains(searchText.ToLower())
                                                || e.Name.ToLower().Contains(searchText.ToLower()));
            }

            List<CustomerDto> customerDtos = new();

            foreach (var customer in allCustomers)
            {
                customerDtos.Add(customer.ConvertToDto());
            }

            return PaginationUtility<CustomerDto>.GetPaginatedResult(in customerDtos, pageIndex == 0 ? 1 : pageIndex, pageSize == 0 ? 4 : pageSize);
        }

        public async Task<CustomerDto> GetSingleCustomerAsync(int id)
        {
            Customer customer = await GetSingleRecordAsync(id);

            return customer.ConvertToDto();
        }

        public async Task UpdateCustomerAsync(CustomerDto customerDto, string imagesFolder)
        {
            Customer customer = await GetSingleRecordAsync(customerDto.Id);

            customer.Code = customerDto.Code;
            customer.Name = customerDto.Name;
            customer.Contact = customerDto.Contact;
            customer.Address = customerDto.Address;
            customer.City = customerDto.City;
            customer.Postal = customerDto.Postal;
            customer.Phone = customerDto.Phone;
            customer.Email = customerDto.Email;
            customer.Web = customerDto.Web;

            if (customerDto.Image != null && !string.IsNullOrEmpty(customer.ImageUrl))
            {
                if (System.IO.File.Exists(Path.Combine(imagesFolder, customer.ImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(imagesFolder, customer.ImageUrl));
                }
            }

            customer.ImageUrl = customerDto.StoreImage(imagesFolder);

            UpdateRecord(customer);
        }

        public async Task<Dictionary<string, string>> ValidateCustomerAsync(CustomerDto customerDto)
        {
            Dictionary<string, string> errors = new();

            var allCustomers = await GetAllRecordsAsync();

            if (customerDto.Id > 0)
            {
                Customer customer = await GetSingleRecordAsync(customerDto.Id);

                if (customer.Code != customerDto.Code)
                {
                    if (allCustomers.Select(e => e.Code.ToLower()).Contains(customerDto.Code.ToLower()))
                    {
                        errors.Add("Code", "There is already Customer with this code in database. Please provide different one!");
                    }
                }

                if (customer.Name != customerDto.Name)
                {
                    if (allCustomers.Select(e => e.Name.ToLower()).Contains(customerDto.Name.ToLower()))
                    {
                        errors.Add("Name", "There is already Customer with this name in database. Please provide different one!");
                    }
                }
            }
            else
            {
                if (allCustomers.Select(e => e.Code.ToLower()).Contains(customerDto.Code.ToLower()))
                {
                    errors.Add("Code", "There is already Customer with this code in database. Please provide different one!");
                }

                if (allCustomers.Select(e => e.Name.ToLower()).Contains(customerDto.Name.ToLower()))
                {
                    errors.Add("Name", "There is already Customer with this name in database. Please provide different one!");
                }
            }

            return errors;
        }

        public async Task<List<string>> GetCustomerNamesAsync()
        {
            List<string> customerList = await context.Customers.Select(e => e.Name).ToListAsync();

            return customerList;
        }
    }
}
