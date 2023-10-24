using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Repositories.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllRecordsAsync();
        Task<T> GetSingleRecordAsync(int id);
        Task CreateNewRecordAsync(T entity);
        void UpdateRecord(T entity);
        Task DeleteRecordAsync(int id);
    }
}
