using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public interface IRepository<TEntity> where TEntity:class
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetAllAsync();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(List<TEntity> entities);
    }
}
