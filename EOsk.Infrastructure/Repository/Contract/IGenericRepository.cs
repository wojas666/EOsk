using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOsk.Infrastructure.Repository.Contract
{
    /// <summary>
    /// Contract for microservices GenericRepository class.
    /// </summary>
    /// <typeparam name="T">Microservice Entity representation class.</typeparam>
    /// <typeparam name="G">Microservice DbContext representation class.</typeparam>
    public interface IGenericRepository<T, G> where T: class where G : class
    {
        Task<IReadOnlyList<T>> Get();

        Task<T> GetById(Guid id);

        Task<T> Add(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task<bool> Exist(Guid id);
    }
}
