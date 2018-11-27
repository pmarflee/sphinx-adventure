using Paramore.Brighter;
using SphinxAdventure.Core.Commands;
using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Infrastructure.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.CommandHandlers
{
    public class CreateUserCommandHandlerAsync : RequestHandlerAsync<CreateUserCommand>
    {
        private readonly IRepository<User> _userRepository;

        public CreateUserCommandHandlerAsync(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async override Task<CreateUserCommand> HandleAsync(
            CreateUserCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var existingUser = await _userRepository.GetByUsernameAsync(command.Username);

            if (existingUser == null)
            {
                var newUser = new User
                {
                    EntityId = command.Id,
                    Username = command.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(command.Password),
                    CreatedOn = command.CreatedOn
                };

                await _userRepository.SaveAsync(newUser);

                command.UserRegistered = true;
            }

            return command;
        }
    }
}
