using Paramore.Darker;
using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Infrastructure;
using SphinxAdventure.Core.Queries;
using SphinxAdventure.Core.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.QueryHandlers
{
    public class GetGamesQueryHandlerAsync : QueryHandlerAsync<GetGamesQuery, IEnumerable<Game>>
    {
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<User> _userRepository;

        public GetGamesQueryHandlerAsync(
            IRepository<Game> gameRepository, IRepository<User> userRepository)
        {
            _gameRepository = gameRepository;
            _userRepository = userRepository;
        }

        public override async Task<IEnumerable<Game>> ExecuteAsync(
            GetGamesQuery query, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var user = await _userRepository.GetAsync(query.UserId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return await _gameRepository.GetAllByUserAsync(user);
        }
    }
}
