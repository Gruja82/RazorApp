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

namespace RazorApp.Services.Repositories.Suppliers
{
    public class SupplierRepository:GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(AppDbContext context):base(context)
        {
            
        }

        public async Task CreateNewSupplierAsync(SupplierDto supplierDto, string imagesFolder)
        {
            Supplier supplier = new();

            supplier.Code = supplierDto.Code;
            supplier.Name = supplierDto.Name;
            supplier.Contact = supplierDto.Contact;
            supplier.Address = supplierDto.Address;
            supplier.City = supplierDto.City;
            supplier.Postal = supplierDto.Postal;
            supplier.Phone = supplierDto.Phone;
            supplier.Email = supplierDto.Email;
            supplier.Web = supplierDto.Web;

            supplier.ImageUrl = supplierDto.StoreImage(imagesFolder);

            await CreateNewRecordAsync(supplier);
        }

        public async Task DeleteSupplierAsync(SupplierDto supplierDto, string imagesFolder)
        {
            Supplier supplier = await GetSingleRecordAsync(supplierDto.Id);

            await DeleteRecordAsync(supplierDto.Id);

            if (!string.IsNullOrEmpty(supplierDto.ImageUrl))
            {
                if (System.IO.File.Exists(Path.Combine(imagesFolder, supplier.ImageUrl!)))
                {
                    System.IO.File.Delete(Path.Combine(imagesFolder, supplier.ImageUrl!));
                }
            }
        }

        public async Task<Pagination<SupplierDto>> GetAllSuppliersAsync(string searchText, int pageIndex, int pageSize)
        {
            var allSuppliers = await GetAllRecordsAsync();

            if (!string.IsNullOrEmpty(searchText))
            {
                allSuppliers = allSuppliers.Where(e => e.Code.ToLower().Contains(searchText.ToLower())
                                                || e.Name.ToLower().Contains(searchText.ToLower()));
            }

            List<SupplierDto> supplierDtos = new();

            foreach (var supplier in allSuppliers)
            {
                supplierDtos.Add(supplier.ConvertToDto());
            }

            return PaginationUtility<SupplierDto>.GetPaginatedResult(in supplierDtos, pageIndex == 0 ? 1 : pageIndex, pageSize == 0 ? 4 : pageSize);
        }

        public async Task<SupplierDto> GetSingleSupplierAsync(int id)
        {
            Supplier supplier = await GetSingleRecordAsync(id);

            return supplier.ConvertToDto();
        }

        public async Task<List<string>> GetSupplierNamesAsync()
        {
            List<string> supplierList = await context.Suppliers.Select(e => e.Name).ToListAsync();

            return supplierList;
        }

        public async Task UpdateSupplierAsync(SupplierDto supplierDto, string imagesFolder)
        {
            Supplier supplier = await GetSingleRecordAsync(supplierDto.Id);

            supplier.Code = supplierDto.Code;
            supplier.Name = supplierDto.Name;
            supplier.Contact = supplierDto.Contact;
            supplier.Address = supplierDto.Address;
            supplier.City = supplierDto.City;
            supplier.Postal = supplierDto.Postal;
            supplier.Phone = supplierDto.Phone;
            supplier.Email = supplierDto.Email;
            supplier.Web = supplierDto.Web;

            if (supplierDto.Image != null && !string.IsNullOrEmpty(supplier.ImageUrl))
            {
                if (System.IO.File.Exists(Path.Combine(imagesFolder, supplier.ImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(imagesFolder, supplier.ImageUrl));
                }
            }

            supplier.ImageUrl = supplierDto.StoreImage(imagesFolder);

            UpdateRecord(supplier);
        }

        public async Task<Dictionary<string, string>> ValidateSupplierAsync(SupplierDto supplierDto)
        {
            Dictionary<string, string> errors = new();

            var allSuppliers = await GetAllRecordsAsync();

            if (supplierDto.Id > 0)
            {
                Supplier supplier = await GetSingleRecordAsync(supplierDto.Id);

                if (supplier.Code != supplierDto.Code)
                {
                    if (allSuppliers.Select(e => e.Code.ToLower()).Contains(supplierDto.Code.ToLower()))
                    {
                        errors.Add("Code", "There is already Supplier with this code in database. Please provide different one!");
                    }
                }

                if (supplier.Name != supplierDto.Name)
                {
                    if (allSuppliers.Select(e => e.Name.ToLower()).Contains(supplierDto.Name.ToLower()))
                    {
                        errors.Add("Name", "There is already Supplier with this name in database. Please provide different one!");
                    }
                }
            }
            else
            {
                if (allSuppliers.Select(e => e.Code.ToLower()).Contains(supplierDto.Code.ToLower()))
                {
                    errors.Add("Code", "There is already Supplier with this code in database. Please provide different one!");
                }

                if (allSuppliers.Select(e => e.Name.ToLower()).Contains(supplierDto.Name.ToLower()))
                {
                    errors.Add("Name", "There is already Supplier with this name in database. Please provide different one!");
                }
            }

            return errors;
        }
    }
}
