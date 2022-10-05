using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using Oredering.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAysyncRepository<T> where T : EntityBase
    {
        protected readonly OrderContext _dbContext;
        private DbSet<T> _query;
        public RepositoryBase(OrderContext dbContext)
        {
            _dbContext = dbContext;
            _query = _dbContext.Set<T>();
        }

        public async Task<T> AddAsync(T Entity)
        {
            await _query.AddAsync(Entity);
            await _dbContext.SaveChangesAsync();
            return Entity;
         }

        public async Task DeleteAsync(T Entity)
        {
            _query.Remove(Entity);
            await _dbContext.SaveChangesAsync();
        }

        //public Task<T> DeleteEntity(T Entity)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predict)
        {
            return await _query.Where(predict).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predict = null, Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null, string includestring = null, bool disableTraaking = true)
        {
            var query = _query.AsQueryable();

            if (disableTraaking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includestring)) query = query.Include(includestring);

            if (predict != null) query = query.Where(predict);

            if (OrderBy != null) 
                return await OrderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predict = null, Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            var query = _query.AsQueryable();

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, included) => current.Include(included));

            if (predict != null) query = query.Where(predict);

            if (OrderBy != null)
                return await OrderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _query.FindAsync(id);
        }

        public async Task UpdateAsync(T Entity)
        {
            _dbContext.Entry(Entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
