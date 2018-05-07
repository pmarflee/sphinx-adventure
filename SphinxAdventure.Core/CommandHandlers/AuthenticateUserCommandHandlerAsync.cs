using Paramore.Brighter;
using SphinxAdventure.Core.Commands;
using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Infrastructure.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.CommandHandlers
{
    public class AuthenticateUserCommandHandlerAsync : RequestHandlerAsync<AuthenticateUserCommand>
    {
        private readonly IRepository<User> _userRepository;

        public AuthenticateUserCommandHandlerAsync(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async override Task<AuthenticateUserCommand> HandleAsync(
            AuthenticateUserCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var user = await _userRepository.GetByUsernameAsync(command.Username);

            if (user != null)
            {
                command.IsValidUsernameAndPassword =
                    BCrypt.Net.BCrypt.Verify(command.Password, user.Password);
            }

            return command;
        }
    }
}
