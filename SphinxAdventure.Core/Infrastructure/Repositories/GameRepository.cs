using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Infrastructure.Indexes.MappedIndexes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YesSql;

namespace SphinxAdventure.Core.Infrastructure.Repositories
{
    public class GameRepository : BaseRepository<Game, GameById>
    {
        public GameRepository(IStore store) : base(store)
        {
        }
    }

    public static class GameRepositoryExtensions
    {
        public static async Task<IEnumerable<Game>> GetAllByUserAsync(
            this IRepository<Game> repository, User user)
        {
            using (var session = repository.CreateSession())
            {
                return await session
                    .Query<Game, GameByUserId>(index => index.UserId == user.EntityId)
                    .ListAsync();
            }
        }
    }
}
