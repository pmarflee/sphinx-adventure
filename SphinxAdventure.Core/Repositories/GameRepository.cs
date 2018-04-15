using System;
using System.Threading.Tasks;
using LiteDB;
using SphinxAdventure.Core.Entities;

namespace SphinxAdventure.Core.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly LiteRepository _repository;

        public GameRepository(LiteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Game> GetAsync(Guid id)
        {
            return _repository.Single<Game>(game => game.Id == id);
        }

        public async Task InsertAsync(Game game)
        {
            _repository.Insert(game);
        }
    }
}
