using System;
using System.Linq;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.Infrastructure.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetAsync(Guid id);
        IOrderedQueryable<TEntity> CreateQuery();
        Task<TEntity> SaveAsync(TEntity entity);
    }
}
