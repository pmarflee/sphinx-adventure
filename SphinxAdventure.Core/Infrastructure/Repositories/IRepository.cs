using System;
using System.Threading.Tasks;
using YesSql;

namespace SphinxAdventure.Core.Infrastructure.Repositories
{
    public interface IRepository<TEntity>
    {
        ISession CreateSession();
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> SaveAsync(TEntity entity);
    }
}
