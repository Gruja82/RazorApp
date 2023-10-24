using Microsoft.EntityFrameworkCore;
using RazorApp.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorApp.Services.Repositories.Generic
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext context;
        public GenericRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task CreateNewRecordAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public async Task DeleteRecordAsync(int id)
        {
            T? entity = await context.Set<T>().FindAsync(id);

            if (entity != null)
            {
                context.Set<T>().Remove(entity);
            }
            else
            {
                throw new ArgumentException("Entity not found!");
            }
        }

        public async Task<IEnumerable<T>> GetAllRecordsAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetSingleRecordAsync(int id)
        {
            T? entity = await context.Set<T>().FindAsync(id);

            if (entity != null)
            {
                return entity;
            }
            else
            {
                throw new ArgumentException("Entity not found!");
            }
        }

        public void UpdateRecord(T entity)
        {
            context.Set<T>().Update(entity);
        }
    }
}
