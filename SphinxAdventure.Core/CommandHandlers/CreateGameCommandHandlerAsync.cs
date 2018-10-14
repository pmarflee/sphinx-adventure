using Paramore.Brighter;
using SphinxAdventure.Core.Commands;
using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Infrastructure.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.CommandHandlers
{
    public class CreateGameCommandHandlerAsync : RequestHandlerAsync<CreateGameCommand>
    {
        private readonly IRepository<Game> _gameRepository;

        public CreateGameCommandHandlerAsync(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public override async Task<CreateGameCommand> HandleAsync(
            CreateGameCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var game = new Game
            {
                EntityId = command.Id,
                UserId = command.UserId,
                CreatedOn = command.CreatedOn
            };

            await _gameRepository.SaveAsync(game);

            return command;
        }
    }
}
