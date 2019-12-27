using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quantum.Core.Entities;

namespace Quantum.Core.Interfaces
{
    public interface IAsyncRepository<T, D> where T : BaseEntity<D>
    {
        Task<T> GetByIdAsync(ISpecification<T> spec);
        Task<T> GetByIdAsync(D id);
        Task<List<T>> ListAllAsync();
        Task<List<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
        Task<bool> IsExists(ISpecification<T> spec);
    }
}
