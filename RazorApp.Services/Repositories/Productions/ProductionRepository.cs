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

namespace RazorApp.Services.Repositories.Productions
{
    public class ProductionRepository:GenericRepository<Production>, IProductionRepository
    {
        public ProductionRepository(AppDbContext context):base(context)
        {
            
        }

        public async Task CreateNewProductionAsync(ProductionDto productionDto)
        {
            Production production = new();

            production.Code = productionDto.Code;
            production.ProductionDate = productionDto.ProductionDate;
            production.Qty = productionDto.Qty;
            production.Product = (await context.Products.FirstOrDefaultAsync(e => e.Name == productionDto.ProductName))!;

            production.Product.Qty += production.Qty;

            //foreach (var productDetail in production.Product.ProductDetails)
            //{
            //    productDetail.Material.Qty -= productDetail.Qty * production.Qty;
            //}

            var productDetails = await context.ProductDetails
                                        .Include(e => e.Product)
                                        .Include(e => e.Material)
                                        .Where(e => e.Product.Name == productionDto.ProductName)
                                        .ToListAsync();

            foreach (var productDetail in productDetails)
            {
                productDetail.Material.Qty -= productDetail.Qty * production.Qty;
            }

            await CreateNewRecordAsync(production);
        }

        public async Task DeleteProductionAsync(int id)
        {
            await DeleteRecordAsync(id);
        }

        public async Task<Pagination<ProductionDto>> GetAllProductionsAsync(string searchText, DateTime productionDate, string product, int pageIndex, int pageSize)
        {
            var allProductions = await context.Productions
                                        .Include(e => e.Product)
                                        .AsNoTracking()
                                        .ToListAsync();

            if(!string.IsNullOrEmpty(searchText))
            {
                allProductions = allProductions.Where(e => e.Code.ToLower().Contains(searchText.ToLower())).ToList();
            }

            if (productionDate != DateTime.MinValue)
            {
                allProductions = allProductions.Where(e => e.ProductionDate.Equals(productionDate)).ToList();
            }

            if (!string.IsNullOrEmpty(product) && product != "Select Product")
            {
                allProductions = allProductions.Where(e => e.Product == context.Products.FirstOrDefault(e => e.Name == product)).ToList();
            }

            List<ProductionDto> productionDtos = new();

            foreach (var production in allProductions)
            {
                productionDtos.Add(production.ConvertToDto());
            }

            return PaginationUtility<ProductionDto>.GetPaginatedResult(in productionDtos, pageIndex == 0 ? 1 : pageIndex, pageSize == 0 ? 4 : pageSize);
        }

        public async Task<ProductionDto> GetSingleProductionAsync(int id)
        {
            Production production = (await context.Productions
                                    .Include(e => e.Product)
                                    .FirstOrDefaultAsync(e => e.Id == id))!;

            return production.ConvertToDto();
        }

        public async Task UpdateProductionAsync(ProductionDto productionDto)
        {
            Production production = (await context.Productions
                                    .Include(e => e.Product)
                                    .ThenInclude(e => e.ProductDetails)
                                    .ThenInclude(e => e.Material)
                                    .FirstOrDefaultAsync(e => e.Id == productionDto.Id))!;

            foreach (var productDetail in production.Product.ProductDetails)
            {
                productDetail.Material.Qty += productDetail.Qty * production.Qty;
            }

            production.Code = productionDto.Code;
            production.ProductionDate = productionDto.ProductionDate;
            production.Qty = productionDto.Qty;
            production.Product = (await context.Products.FirstOrDefaultAsync(e => e.Name == productionDto.ProductName))!;

            foreach (var productDetail in production.Product.ProductDetails)
            {
                productDetail.Material.Qty -= productDetail.Qty * production.Qty;
            }
        }

        public async Task<Dictionary<string, string>> ValidateProductionAsync(ProductionDto productionDto)
        {
            Dictionary<string, string> errors = new();

            var allProductions = await context.Productions
                                .Include(e => e.Product)
                                .ThenInclude(e => e.ProductDetails)
                                .ThenInclude(e => e.Material)
                                .AsNoTracking()
                                .ToListAsync();

            if (productionDto.Id > 0)
            {
                Production production = (await context.Productions
                                        .Include(e => e.Product)
                                        .ThenInclude(e => e.ProductDetails)
                                        .ThenInclude(e => e.Material)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(e=>e.Id==productionDto.Id))!;

                if (productionDto.Code != production.Code)
                {
                    if (allProductions.Select(e => e.Code.ToLower()).Contains(productionDto.Code.ToLower()))
                    {
                        errors.Add("Code", "There is already Production with this code in database. Please provide different one!");
                    }

                    if (productionDto.ProductionDate > DateTime.Now)
                    {
                        errors.Add("ProductionDate", "Production date must not be larger than today's date!");
                    }

                    foreach (var productDetail in production.Product.ProductDetails)
                    {
                        if (productionDto.Qty * productDetail.Qty > productDetail.Material.Qty)
                        {
                            errors.Add("ProductName", "There is not enough material for this production!");
                        }
                    }
                }
            }
            else
            {
                if (allProductions.Select(e => e.Code.ToLower()).Contains(productionDto.Code.ToLower()))
                {
                    errors.Add("Code", "There is already Production with this code in database. Please provide different one!");
                }

                if (productionDto.ProductionDate > DateTime.Now)
                {
                    errors.Add("ProductionDate", "Production date must not be larger than today's date!");
                }

                Product product = (await context.Products
                                        .Include(e => e.ProductDetails)
                                        .ThenInclude(e => e.Material)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(e => e.Name == productionDto.ProductName))!;

                foreach (var productDetail in product.ProductDetails)
                {
                    if (productionDto.Qty * productDetail.Qty > productDetail.Material.Qty)
                    {
                        errors.Add("Product", "There is not enough material for this production!");
                    }
                }
            }

            if (productionDto.Qty <= 0)
            {
                errors.Add("Qty", "Quantity must larger than 0 (zero)!");
            }

            return errors;
        }

        public async Task<List<DateTime>> GetProductionDatesAsync()
        {
            var allProductions = await context.Productions.ToListAsync();

            List<DateTime> productionDates = new();

            foreach (var production in allProductions)
            {
                productionDates.Add(production.ProductionDate);
            }

            return productionDates;
        }
    }
}
