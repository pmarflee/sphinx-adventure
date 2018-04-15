using Paramore.Brighter;
using SphinxAdventure.Core.Commands;
using SphinxAdventure.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.CommandHandlers
{
    public class AuthenticateUserCommandHandlerAsync : RequestHandlerAsync<AuthenticateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public AuthenticateUserCommandHandlerAsync(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async override Task<AuthenticateUserCommand> HandleAsync(
            AuthenticateUserCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var user = await _userRepository.GetAsync(command.Username);

            if (user != null)
            {
                command.IsValidUsernameAndPassword =
                    BCrypt.Net.BCrypt.Verify(command.Password, user.Password);
            }

            return command;
        }
    }
}
