using Paramore.Darker;
using SphinxAdventure.Core.Queries;
using SphinxAdventure.Core.Infrastructure.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.QueryHandlers
{
    public class GetGameQueryHandlerAsync : QueryHandlerAsync<GetGameQuery, DTOs.Game>
    {
        private readonly IRepository<Entities.Game> _gameRepository;

        public GetGameQueryHandlerAsync(IRepository<Entities.Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public override async Task<DTOs.Game> ExecuteAsync(
            GetGameQuery query, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var gameEntity = await _gameRepository.GetAsync(query.Id);

            return new DTOs.Game
            {
                Id = gameEntity.EntityId,
                CreatedOn = gameEntity.CreatedOn
            };
        }
    }
}
