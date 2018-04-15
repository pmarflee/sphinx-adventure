using SphinxAdventure.Core.Entities;
using System;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.Repositories
{
    public interface IGameRepository
    {
        Task<Game> GetAsync(Guid id);
        Task InsertAsync(Game game);
    }
}
