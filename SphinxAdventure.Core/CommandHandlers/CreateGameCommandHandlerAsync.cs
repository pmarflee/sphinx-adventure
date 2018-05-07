using Paramore.Brighter;
using SphinxAdventure.Core.Commands;
using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.CommandHandlers
{
    public class CreateGameCommandHandlerAsync : RequestHandlerAsync<CreateGameCommand>
    {
        private readonly IRepository<Game> _gameRepository;
        private readonly IUserRepository _userRepository;

        public CreateGameCommandHandlerAsync(
            IRepository<Game> gameRepository,
            IUserRepository userRepository)
        {
            _gameRepository = gameRepository;
            _userRepository = userRepository;
        }

        public override async Task<CreateGameCommand> HandleAsync(
            CreateGameCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var user = await _userRepository.GetAsync(command.Username);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var game = new Game
            {
                Id = command.Id,
                UserId = user.Id,
                CreatedOn = command.CreatedOn
            };

            await _gameRepository.SaveAsync(game);

            return command;
        }
    }
}
