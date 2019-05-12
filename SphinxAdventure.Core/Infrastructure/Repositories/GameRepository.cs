using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Infrastructure.Indexes.MappedIndexes;
using SphinxAdventure.Core.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YesSql;

namespace SphinxAdventure.Core.Infrastructure.Repositories
{
    public class GameRepository : BaseRepository<Game, GameById>
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public GameRepository(IStore store, IRandomNumberGenerator randomNumberGenerator) : base(store)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        public async override Task<Game> GetAsync(Guid id)
        {
            var game = await base.GetAsync(id);
            game.NextRandomNumber = _randomNumberGenerator.Next;

            return game;
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
