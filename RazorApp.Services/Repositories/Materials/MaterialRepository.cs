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

namespace RazorApp.Services.Repositories.Materials
{
    public class MaterialRepository:GenericRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(AppDbContext context):base(context)
        {
            
        }

        public async Task CreateNewMaterialAsync(MaterialDto materialDto, string imagesFolder)
        {
            Material material = new();

            material.Code = materialDto.Code;
            material.Name = materialDto.Name;
            material.Category = (await context.Categories.FirstOrDefaultAsync(e => e.Name == materialDto.CategoryName))!;
            material.Qty = 0;
            material.Price = materialDto.Price;
            material.ImageUrl = materialDto.StoreImage(imagesFolder);

            await CreateNewRecordAsync(material);
        }

        public async Task DeleteMaterialAsync(MaterialDto materialDto, string imagesFolder)
        {
            Material material = await GetSingleRecordAsync(materialDto.Id);

            if (!string.IsNullOrEmpty(material.ImageUrl))
            {
                if (System.IO.File.Exists(Path.Combine(imagesFolder, material.ImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(imagesFolder, material.ImageUrl));
                }
            }

            await DeleteRecordAsync(material.Id);
        }

        public async Task<Pagination<MaterialDto>> GetAllMaterialsAsync(string searchText, string category, int pageIndex, int pageSize)
        {
            var allMaterials = await context.Materials
                                .Include(e => e.Category)
                                .AsNoTracking()
                                .ToListAsync();

            if(!string.IsNullOrEmpty(searchText))
            {
                allMaterials = allMaterials.Where(e=>e.Code.ToLower().Contains(searchText.ToLower())
                                                || e.Name.ToLower().Contains(searchText.ToLower()))
                                                .ToList();          
            }

            if(!string.IsNullOrEmpty(category) && category != "Select Category")
            {
                allMaterials = allMaterials.Where(e => e.Category == context.Categories.FirstOrDefault(e => e.Name == category)).ToList();
            }

            List<MaterialDto> materialDtos = new();

            foreach (var material in allMaterials)
            {
                materialDtos.Add(material.ConvertToDto());
            }

            return PaginationUtility<MaterialDto>.GetPaginatedResult(in materialDtos, pageIndex == 0 ? 1 : pageIndex, pageSize == 0 ? 4 : pageSize);
        }

        public async Task<MaterialDto> GetSingleMaterialAsync(int id)
        {
            Material material = (await context.Materials
                                .Include(e => e.Category)
                                .FirstOrDefaultAsync(e => e.Id == id))!;

            return material.ConvertToDto();
        }

        public async Task UpdateMaterialAsync(MaterialDto materialDto, string imagesFolder)
        {
            Material material = (await context.Materials
                                .Include(e => e.Category)
                                .FirstOrDefaultAsync(e => e.Id == materialDto.Id))!;

            material.Code = materialDto.Code;
            material.Name = materialDto.Name;
            material.Category = (await context.Categories.FirstOrDefaultAsync(e => e.Name == materialDto.CategoryName))!;
            material.Price = materialDto.Price;

            if (materialDto.Image != null && !string.IsNullOrEmpty(material.ImageUrl))
            {
                if (System.IO.File.Exists(Path.Combine(imagesFolder, material.ImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(imagesFolder, material.ImageUrl));
                }
            }

            material.ImageUrl = materialDto.StoreImage(imagesFolder);

            UpdateRecord(material);
        }

        public async Task<Dictionary<string, string>> ValidateMaterialAsync(MaterialDto materialDto)
        {
            Dictionary<string, string> errors = new();

            var allMaterials = await GetAllRecordsAsync();

            if (materialDto.Id > 0)
            {
                Material material = await GetSingleRecordAsync(materialDto.Id);

                if (material.Code != materialDto.Code)
                {
                    if (allMaterials.Select(e => e.Code.ToLower()).Contains(materialDto.Code.ToLower()))
                    {
                        errors.Add("Code", "There is already Material with this code in database. Please provide different one!");
                    }
                }

                if (material.Name != materialDto.Name)
                {
                    if (allMaterials.Select(e => e.Name.ToLower()).Contains(materialDto.Name.ToLower()))
                    {
                        errors.Add("Name", "There is already Material with this name in database. Please provide different one!");
                    }
                }
            }
            else
            {
                if (allMaterials.Select(e => e.Code.ToLower()).Contains(materialDto.Code.ToLower()))
                {
                    errors.Add("Code", "There is already Material with this code in database. Please provide different one!");
                }

                if (allMaterials.Select(e => e.Name.ToLower()).Contains(materialDto.Name.ToLower()))
                {
                    errors.Add("Name", "There is already Material with this name in database. Please provide different one!");
                }
            }

            return errors;
        }

        public async Task<List<MaterialDto>> GetMaterialDtos()
        {
            var allMaterials = await context.Materials
                                    .Include(e => e.Category)
                                    .ToListAsync();

            List<MaterialDto> materialDtos = new();

            foreach (var material in allMaterials)
            {
                materialDtos.Add(material.ConvertToDto());
            }

            return materialDtos;
        }
    }
}
