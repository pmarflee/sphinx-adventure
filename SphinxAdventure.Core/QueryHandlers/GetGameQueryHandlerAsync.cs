using Paramore.Darker;
using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Queries;
using SphinxAdventure.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.QueryHandlers
{
    public class GetGameQueryHandlerAsync : QueryHandlerAsync<GetGameQuery, Game>
    {
        private readonly IGameRepository _gameRepository;

        public GetGameQueryHandlerAsync(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public override async Task<Game> ExecuteAsync(
            GetGameQuery query, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _gameRepository.GetAsync(query.Id);
        }
    }
}
