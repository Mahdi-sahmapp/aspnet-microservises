

using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oredering.Application.Contracts.Persistence
{
    public interface IAysyncRepository<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predict);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predict = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null,
                                        string includestring = null, bool disableTraaking = true);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predict = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null,
                                        List<Expression<Func<T,object>>> includes = null,
                                        bool disableTracking = true);

        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T Entity);
        Task UpdateAsync(T Entity);
        //Task<T> DeleteEntity(T Entity);
        Task DeleteAsync(T Entity);

    }
}
