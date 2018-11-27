using Paramore.Darker;
using SphinxAdventure.Core.Infrastructure.Repositories;
using SphinxAdventure.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.QueryHandlers
{
    public class GetGamesQueryHandlerAsync : QueryHandlerAsync<GetGamesQuery, IEnumerable<DTOs.Game>>
    {
        private readonly IRepository<Entities.Game> _gameRepository;
        private readonly IRepository<Entities.User> _userRepository;

        public GetGamesQueryHandlerAsync(
            IRepository<Entities.Game> gameRepository, IRepository<Entities.User> userRepository)
        {
            _gameRepository = gameRepository;
            _userRepository = userRepository;
        }

        public override async Task<IEnumerable<DTOs.Game>> ExecuteAsync(
            GetGamesQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var user = await _userRepository.GetAsync(query.UserId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return (from game in await _gameRepository.GetAllByUserAsync(user)
                    select new DTOs.Game
                    {
                        Id = game.EntityId,
                        CreatedOn = game.CreatedOn
                    }).ToList();
        }
    }
}
