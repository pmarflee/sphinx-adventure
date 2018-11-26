using Paramore.Brighter;
using SphinxAdventure.Core.Commands;
using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Factories;
using SphinxAdventure.Core.Infrastructure.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.CommandHandlers
{
    public class CreateGameCommandHandlerAsync : RequestHandlerAsync<CreateGameCommand>
    {
        private readonly IFactory<Map> _mapFactory;
        private readonly IRepository<Game> _gameRepository;

        public CreateGameCommandHandlerAsync(
            IFactory<Map> mapFactory,
            IRepository<Game> gameRepository)
        {
            _mapFactory = mapFactory;
            _gameRepository = gameRepository;
        }

        public override async Task<CreateGameCommand> HandleAsync(
            CreateGameCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var map = _mapFactory.Create();

            var game = new Game
            {
                EntityId = command.Id,
                UserId = command.UserId,
                Map = map,
                Location = map.Locations.Values.First(),
                CreatedOn = command.CreatedOn
            };

            await _gameRepository.SaveAsync(game);

            return command;
        }
    }
}
