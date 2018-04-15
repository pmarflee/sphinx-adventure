using Paramore.Brighter;
using System;

namespace SphinxAdventure.Core.Commands
{
    public class AuthenticateUserCommand : IRequest
    {
        public AuthenticateUserCommand(string username, string password)
        {
            Id = Guid.NewGuid();

            Username = username;
            Password = password;
        }

        public Guid Id { get; set; }

        public string Username { get; }
        public string Password { get; }

        public bool IsValidUsernameAndPassword { get; set; }
    }
}
