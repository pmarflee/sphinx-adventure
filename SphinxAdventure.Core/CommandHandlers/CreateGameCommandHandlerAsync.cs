using Paramore.Brighter;
using SphinxAdventure.Core.Commands;
using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.CommandHandlers
{
    public class CreateGameCommandHandlerAsync : RequestHandlerAsync<CreateGameCommand>
    {
        private readonly IGameRepository _gameRepository;

        public CreateGameCommandHandlerAsync(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public override async Task<CreateGameCommand> HandleAsync(
            CreateGameCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var game = new Game { Id = command.Id };

            await _gameRepository.InsertAsync(game);

            return command;
        }
    }
}
