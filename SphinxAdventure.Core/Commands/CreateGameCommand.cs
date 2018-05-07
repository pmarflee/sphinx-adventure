using Paramore.Brighter;
using System;

namespace SphinxAdventure.Core.Commands
{
    public class CreateGameCommand : IRequest
    {
        public CreateGameCommand(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CreatedOn = DateTime.Now;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; }
        public DateTime CreatedOn { get; set; }
    }
}
