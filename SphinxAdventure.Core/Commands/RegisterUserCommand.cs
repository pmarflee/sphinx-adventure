using Paramore.Brighter;
using System;

namespace SphinxAdventure.Core.Commands
{
    public class CreateUserCommand : IRequest
    {
        public CreateUserCommand(string username, string password)
        {
            Username = username;
            Password = password;
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Username { get; }
        public string Password { get; }
        public DateTime CreatedOn { get; set; }

        public bool UserRegistered { get; set; }
    }
}
