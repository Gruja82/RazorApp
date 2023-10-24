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

namespace RazorApp.Services.Repositories.Purchases
{
    public class PurchaseRepository:GenericRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(AppDbContext context):base(context)
        {
            
        }

        public async Task CreateNewPurchaseAsync(PurchaseDto purchaseDto)
        {
            Purchase purchase = new();

            purchase.Code = purchaseDto.PurchaseCode;
            purchase.PurchaseDate = purchaseDto.PurchaseDate;
            purchase.Supplier = (await context.Suppliers.FirstOrDefaultAsync(e => e.Name == purchaseDto.SupplierName))!;

            foreach (var purchaseDetailDto in purchaseDto.PurchaseDetailDtos)
            {
                PurchaseDetail purchaseDetail = new();

                purchaseDetail.Purchase = purchase;
                purchaseDetail.Material = (await context.Materials.FirstOrDefaultAsync(e => e.Name == purchaseDetailDto.MaterialName))!;
                purchaseDetail.Qty = purchaseDetailDto.Qty;
                purchaseDetail.Material.Qty += purchaseDetailDto.Qty;

                await context.PurchaseDetails.AddAsync(purchaseDetail);
            }

            await CreateNewRecordAsync(purchase);
        }

        public async Task DeletePurchaseAsync(int id)
        {
            Purchase purchase = (await context.Purchases
                                        .Include(e => e.PurchaseDetails)
                                        .ThenInclude(e => e.Material)
                                        .FirstOrDefaultAsync(e => e.Id == id))!;

            foreach (var purchaseDetail in purchase.PurchaseDetails)
            {
                purchaseDetail.Material.Qty -= purchaseDetail.Qty;

                context.PurchaseDetails.Remove(purchaseDetail);
            }

            await DeleteRecordAsync(id);
        }

        public async Task<Pagination<PurchaseDto>> GetAllPurchasesAsync(string searchText, string supplier, int pageIndex, int pageSize)
        {
            var allPurchases = await context.Purchases
                                    .Include(e => e.Supplier)
                                    .Include(e => e.PurchaseDetails)
                                    .ThenInclude(e => e.Material)
                                    .AsNoTracking()
                                    .ToListAsync();

            if (!string.IsNullOrEmpty(searchText))
            {
                allPurchases = allPurchases.Where(e => e.Code.ToLower().Contains(searchText.ToLower())).ToList();
            }

            if(!string.IsNullOrEmpty(supplier) && supplier!="Select Supplier")
            {
                allPurchases = allPurchases.Where(e => e.Supplier == context.Suppliers.FirstOrDefault(e => e.Name == supplier)).ToList();
            }

            List<PurchaseDto> purchaseDtos = new();

            foreach (var purchase in allPurchases)
            {
                purchaseDtos.Add(purchase.ConvertToDto());
            }

            return PaginationUtility<PurchaseDto>.GetPaginatedResult(in purchaseDtos, pageIndex == 0 ? 1 : pageIndex, pageSize == 0 ? 4 : pageSize);
        }

        public async Task<PurchaseDto> GetSinglePurchaseAsync(int id)
        {
            Purchase purchase = (await context.Purchases
                                        .Include(e => e.Supplier)
                                        .Include(e => e.PurchaseDetails)
                                        .ThenInclude(e => e.Material)
                                        .FirstOrDefaultAsync(e => e.Id == id))!;

            return purchase.ConvertToDto();
        }

        public async Task UpdatePurchaseAsync(PurchaseDto purchaseDto)
        {
            Purchase purchase = (await context.Purchases
                                        .Include(e => e.Supplier)
                                        .Include(e => e.PurchaseDetails)
                                        .ThenInclude(e => e.Material)
                                        .FirstOrDefaultAsync(e => e.Id == purchaseDto.Id))!;

            purchase.Code = purchaseDto.PurchaseCode;
            purchase.PurchaseDate = purchaseDto.PurchaseDate;
            purchase.Supplier = (await context.Suppliers.FirstOrDefaultAsync(e => e.Name == purchaseDto.SupplierName))!;

            foreach (var purchaseDetail in purchase.PurchaseDetails)
            {
                purchaseDetail.Material.Qty -= purchaseDetail.Qty;

                context.PurchaseDetails.Remove(purchaseDetail);
            }

            foreach (var purchaseDetailDto in purchaseDto.PurchaseDetailDtos)
            {
                PurchaseDetail purchaseDetail = new();

                purchaseDetail.Purchase = purchase;
                purchaseDetail.Material = (await context.Materials.FirstOrDefaultAsync(e => e.Name == purchaseDetailDto.MaterialName))!;
                purchaseDetail.Qty = purchaseDetailDto.Qty;
                purchaseDetail.Material.Qty += purchaseDetailDto.Qty;

                await context.PurchaseDetails.AddAsync(purchaseDetail);
            }
        }

        public async Task<Dictionary<string, string>> ValidatePurchaseAsync(PurchaseDto purchaseDto)
        {
            Dictionary<string, string> errors = new();

            var allPurchases = await context.Purchases
                                    .Include(e => e.PurchaseDetails)
                                    .ThenInclude(e => e.Material)
                                    .ToListAsync();

            if (purchaseDto.Id > 0)
            {
                Purchase purchase = (await context.Purchases
                                            .Include(e => e.PurchaseDetails)
                                            .ThenInclude(e => e.Material)
                                            .FirstOrDefaultAsync(e => e.Id == purchaseDto.Id))!;

                if (purchase.Code != purchaseDto.PurchaseCode)
                {
                    if (allPurchases.Select(e => e.Code.ToLower()).Contains(purchaseDto.PurchaseCode.ToLower()))
                    {
                        errors.Add("PurchaseCode", "There is already Purchase with this code in database. Please provide different one!");
                    }
                }

                if (purchaseDto.PurchaseDate > DateTime.Now)
                {
                    errors.Add("PurchaseDate", "Purchase date must not be larger than today's date!");
                }

                if(purchaseDto.PurchaseDetailDtos == null || purchaseDto.PurchaseDetailDtos.Count <= 0)
                {
                    errors.Add("PurchaseDetailDtos", "There must be at least one Material in purchase list!");
                }
            }
            else
            {
                if (allPurchases.Select(e => e.Code.ToLower()).Contains(purchaseDto.PurchaseCode.ToLower()))
                {
                    errors.Add("PurchaseCode", "There is already Purchase with this code in database. Please provide different one!");
                }

                if (purchaseDto.PurchaseDate > DateTime.Now)
                {
                    errors.Add("PurchaseDate", "Purchase date must not be larger than today's date!");
                }

                if (purchaseDto.PurchaseDetailDtos == null || purchaseDto.PurchaseDetailDtos.Count <= 0)
                {
                    errors.Add("PurchaseDetailDtos", "There must be at least one Material in purchase list!");
                }
            }

            return errors;
        }
    }
}
