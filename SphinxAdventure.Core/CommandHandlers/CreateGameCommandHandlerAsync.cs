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
        private readonly IFactory<Game, GameProps> _gameFactory;
        private readonly IRepository<Game> _gameRepository;

        public CreateGameCommandHandlerAsync(
            IFactory<Game, GameProps> gameFactory,
            IRepository<Game> gameRepository)
        {
            _gameFactory = gameFactory;
            _gameRepository = gameRepository;
        }

        public override async Task<CreateGameCommand> HandleAsync(
            CreateGameCommand command, 
            CancellationToken cancellationToken = default)
        {
            var game = _gameFactory.Create(new GameProps(command.Id, command.UserId, command.CreatedOn));

            await _gameRepository.SaveAsync(game);

            return command;
        }
    }
}
