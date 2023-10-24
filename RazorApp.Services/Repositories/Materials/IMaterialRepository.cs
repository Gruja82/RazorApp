using RazorApp.Data.Entities;
using RazorApp.Models.Dtos;
using RazorApp.Services.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Repositories.Materials
{
    public interface IMaterialRepository:IGenericRepository<Material>
    {
        Task<Pagination<MaterialDto>> GetAllMaterialsAsync(string searchText, string category, int pageIndex, int pageSize);
        Task<MaterialDto> GetSingleMaterialAsync(int id);
        Task CreateNewMaterialAsync(MaterialDto materialDto, string imagesFolder);
        Task UpdateMaterialAsync(MaterialDto materialDto, string imagesFolder);
        Task DeleteMaterialAsync(MaterialDto materialDto, string imagesFolder);
        Task<Dictionary<string, string>> ValidateMaterialAsync(MaterialDto materialDto);
        Task<List<MaterialDto>> GetMaterialDtos();
    }
}
