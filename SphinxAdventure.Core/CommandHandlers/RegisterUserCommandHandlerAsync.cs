﻿using Paramore.Brighter;
using SphinxAdventure.Core.Commands;
using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.CommandHandlers
{
    public class RegisterUserCommandHandlerAsync : RequestHandlerAsync<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandlerAsync(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async override Task<RegisterUserCommand> HandleAsync(
            RegisterUserCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var existingUser = await _userRepository.GetAsync(command.Username);

            if (existingUser == null)
            {
                var newUser = new User
                {
                    Id = command.Id,
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
