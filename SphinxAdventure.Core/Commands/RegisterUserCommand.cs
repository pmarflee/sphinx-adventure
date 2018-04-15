using Paramore.Brighter;
using System;

namespace SphinxAdventure.Core.Commands
{
    public class RegisterUserCommand : IRequest
    {
        public RegisterUserCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public Guid Id { get; set; }
        public string Username { get; }
        public string Password { get; }

        public bool UserRegistered { get; set; }
    }
}
