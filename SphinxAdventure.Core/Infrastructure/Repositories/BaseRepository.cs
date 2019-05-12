using System;
using System.Threading.Tasks;
using YesSql;

namespace SphinxAdventure.Core.Infrastructure.Repositories
{
    public class BaseRepository<TEntity, TEntityByIdIndex> : IRepository<TEntity>
        where TEntity : class
        where TEntityByIdIndex : Indexes.MappedIndexes.EntityById
    {
        protected IStore Store { get; }

        protected BaseRepository(IStore store)
        {
            Store = store;
        }

        public ISession CreateSession() => Store.CreateSession();

        public async virtual Task<TEntity> GetAsync(Guid id)
        {
            using (var session = Store.CreateSession())
            {
                return await session.Query<TEntity, TEntityByIdIndex>()
                    .Where(index => index.EntityId == id).FirstOrDefaultAsync();
            }
        }
                
        public async virtual Task<TEntity> SaveAsync(TEntity entity)
        {
            using (var session = Store.CreateSession())
            {
                session.Save(entity);

                await session.CommitAsync();
            }

            return entity;
        }
    }
}
