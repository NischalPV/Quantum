using Microsoft.EntityFrameworkCore;
using Quantum.Core.Entities;
using Quantum.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Infrastructure.Data.Repositories
{
    public class EfRepository<T, D> : IAsyncRepository<T, D> where T : BaseEntity<D>
    {
        protected readonly QuantumDbContext _dbContext;
        private readonly IAppLogger<EfRepository<T, D>> _logger;

        public EfRepository(QuantumDbContext dbContext, IAppLogger<EfRepository<T, D>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot add entity of type {entity.GetType()}. Error: [{ex.Message}]");
            }
            return entity;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            entity.IsActive = false;
            _dbContext.Entry(entity).State = EntityState.Modified;
            bool result;
            try
            {
                await _dbContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot delete entity of type {entity.GetType()}. Error: [{ex.Message}]");
                result = false;
            }
            return result;

        }

        public async Task<T> GetByIdAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(D id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> IsExists(ISpecification<T> spec)
        {
            var result = await ApplySpecification(spec).FirstOrDefaultAsync();
            if (result != null) return true;
            else return false;
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            _dbContext.Entry(entity).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cannot update entity of type {entity.GetType()}. Error: [{ex.Message}]");
            }

            return entity;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T, D>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
