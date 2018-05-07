using Paramore.Brighter;
using System;

namespace SphinxAdventure.Core.Commands
{
    public class CreateGameCommand : IRequest
    {
        public CreateGameCommand(string username)
        {
            Id = Guid.NewGuid();
            Username = username;
            CreatedOn = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Username { get; }
        public DateTime CreatedOn { get; set; }
    }
}
