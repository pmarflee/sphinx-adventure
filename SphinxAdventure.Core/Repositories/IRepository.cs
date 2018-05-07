using System;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> SaveAsync(TEntity entity);
    }
}
