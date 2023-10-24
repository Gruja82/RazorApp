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

namespace RazorApp.Services.Repositories.Products
{
    public class ProductRepository:GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context):base(context)
        {
            
        }

        public async Task CreateNewProductAsync(ProductDto productDto, string imagesFolder)
        {
            Product product = new();

            product.Code = productDto.Code;
            product.Name = productDto.Name;
            product.Category = (await context.Categories.FirstOrDefaultAsync(e => e.Name == productDto.CategoryName))!;
            product.Qty = 0;
            product.Price = productDto.Price;
            product.ImageUrl = productDto.StoreImage(imagesFolder);

            foreach (var productDetailDto in productDto.ProductDetailDtos)
            {
                ProductDetail productDetail = new();

                productDetail.Product = product;
                productDetail.Material = (await context.Materials.FirstOrDefaultAsync(e => e.Name == productDetailDto.MaterialName))!;
                productDetail.Qty = productDetailDto.Qty;

                await context.ProductDetails.AddAsync(productDetail);
            }

            await CreateNewRecordAsync(product);
        }

        public async Task DeleteProductAsync(ProductDto productDto, string imagesFolder)
        {
            Product product = (await context.Products
                                    .Include(e => e.ProductDetails)
                                    .FirstOrDefaultAsync(e => e.Id == productDto.Id))!;

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                if(System.IO.File.Exists(Path.Combine(imagesFolder, product.ImageUrl)))
                {
                    System.IO.File.Delete((Path.Combine(imagesFolder, product.ImageUrl))); 
                }
            }
        }

        public async Task<Pagination<ProductDto>> GetAllProductsAsync(string searchText, string category, int pageIndex, int pageSize)
        {
            var allProducts = await context.Products
                                    .Include(e => e.Category)
                                    .Include(e => e.ProductDetails)
                                    .ThenInclude(e => e.Material)
                                    .AsNoTracking()
                                    .ToListAsync();

            if (!string.IsNullOrEmpty(searchText))
            {
                allProducts = allProducts.Where(e => e.Code.ToLower().Contains(searchText.ToLower())
                                                || e.Name.ToLower().Contains(searchText.ToLower()))
                                                .ToList();
            }

            if (!string.IsNullOrEmpty(category) && category != "Select Category")
            {
                allProducts = allProducts.Where(e => e.Category == context.Categories.FirstOrDefault(e => e.Name == category)).ToList();
            }

            List<ProductDto> productDtos = new();

            foreach (var product in allProducts)
            {
                productDtos.Add(product.ConvertToDto());
            }

            return PaginationUtility<ProductDto>.GetPaginatedResult(in productDtos, pageIndex == 0 ? 1 : pageIndex, pageSize == 0 ? 4 : pageSize);
        }

        public async Task<ProductDto> GetSingleProductAsync(int id)
        {
            Product product = (await context.Products
                                .Include(e => e.Category)
                                .Include(e => e.ProductDetails)
                                .ThenInclude(e => e.Material)
                                .FirstOrDefaultAsync(e => e.Id == id))!;

            return product.ConvertToDto();
        }

        public async Task UpdateProductAsync(ProductDto productDto, string imagesFolder)
        {
            Product product = (await context.Products
                                .Include(e => e.Category)
                                .Include(e => e.ProductDetails)
                                .ThenInclude(e => e.Material)
                                .FirstOrDefaultAsync(e => e.Id == productDto.Id))!;

            product.Code = productDto.Code;
            product.Name = productDto.Name;
            product.Category = (await context.Categories.FirstOrDefaultAsync(e => e.Name == productDto.CategoryName))!;
            product.Price = productDto.Price;

            foreach (var productDetail in product.ProductDetails)
            {
                context.ProductDetails.Remove(productDetail);
            }

            foreach (var productDetailDto in productDto.ProductDetailDtos!)
            {
                ProductDetail productDetail = new();

                productDetail.Product = product;
                productDetail.Material = (await context.Materials.FirstOrDefaultAsync(e => e.Name == productDetailDto.MaterialName))!;
                productDetail.Qty = productDetailDto.Qty;

                context.ProductDetails.Add(productDetail);
            }

            UpdateRecord(product);
        }

        public async Task<Dictionary<string, string>> ValidateProductAsync(ProductDto productDto)
        {
            Dictionary<string, string> errors = new();

            var allProducts = await context.Products
                                    .Include(e => e.ProductDetails)
                                    .ToListAsync();

            if (productDto.Id > 0)
            {
                Product product = (allProducts.FirstOrDefault(e => e.Id == productDto.Id))!;

                if (product.Code != productDto.Code)
                {
                    if (allProducts.Select(e => e.Code.ToLower()).Contains(productDto.Code.ToLower()))
                    {
                        errors.Add("Code", "There is already Product with this code in database. Please provide different one!");
                    }
                }

                if (product.Name != productDto.Name)
                {
                    if (allProducts.Select(e => e.Name.ToLower()).Contains(productDto.Name.ToLower()))
                    {
                        errors.Add("Name", "There is already Product with this name in database. Please provide different one!");
                    }
                }

                if (productDto.ProductDetailDtos == null || productDto.ProductDetailDtos!.Count <= 0)
                {
                    errors.Add("ProductDetailDtos", "There must be at least one Material in production specification!");
                }

                if (productDto.ProductDetailDtos?.Where(e => e.Qty > 0).Count() == 0)
                {
                    errors.Add("ProductDetailDtos", "There must be at least one Material with quantity larger than 0 in product specification!");
                }
            }
            else
            {
                if (allProducts.Select(e => e.Code.ToLower()).Contains(productDto.Code.ToLower()))
                {
                    errors.Add("Code", "There is already Product with this code in database. Please provide different one!");
                }

                if (allProducts.Select(e => e.Name.ToLower()).Contains(productDto.Name.ToLower()))
                {
                    errors.Add("Name", "There is already Product with this name in database. Please provide different one!");
                }

                if (productDto.ProductDetailDtos == null || productDto.ProductDetailDtos!.Count <= 0)
                {
                    errors.Add("ProductDetailDtos", "There must be at least one Material in production specification!");
                }

                if (productDto.ProductDetailDtos?.Where(e => e.Qty > 0).Count() == 0)
                {
                    errors.Add("ProductDetailDtos", "There must be at least one Material with quantity larger than 0 in product specification!");
                }
            }

            return errors;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var allProducts = await context.Products
                                    .Include(e => e.Category)
                                    .Include(e => e.ProductDetails)
                                    .ThenInclude(e => e.Material)
                                    .Where(e => e.Qty > 0)
                                    .ToListAsync();

            List<ProductDto> productDtos = new();

            foreach (var product in allProducts)
            {
                productDtos.Add(product.ConvertToDto());
            }

            return productDtos;
        }
    }
}
