using Paramore.Brighter;
using System;

namespace SphinxAdventure.Core.Commands
{
    public class CreateGameCommand : IRequest
    {
        public CreateGameCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
