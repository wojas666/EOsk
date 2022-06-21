using EOsk.Infrastructure.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOsk.Infrastructure.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Microservice Entity representation class.</typeparam>
    /// <typeparam name="G">Microservice DbContext representation class.</typeparam>
    public class GenericRepository<T, G> : IGenericRepository<T,G> where T : class where G : class
    {
        private readonly G _dbContext;

        public GenericRepository(G dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Add(T entity)
        {
            await (_dbContext as DbContext).Set<T>().AddAsync(entity);
            await (_dbContext as DbContext).SaveChangesAsync();

            return entity;
        }

        public async Task Delete(T entity)
        {
            (_dbContext as DbContext).Set<T>().Remove(entity);
            await (_dbContext as DbContext).SaveChangesAsync();
        }

        public async Task<bool> Exist(Guid id)
        {
            var element = await GetById(id);

            if (element != null)
                return true;
            else
                return false;
        }

        public async Task<IReadOnlyList<T>> Get()
        {
            return await (_dbContext as DbContext).Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await (_dbContext as DbContext).Set<T>().FindAsync(id);
        }

        public virtual async Task Update(T entity)
        {
            (_dbContext as DbContext).Entry(entity).State = EntityState.Modified;

            await (_dbContext as DbContext).SaveChangesAsync();
        }
    }
}
