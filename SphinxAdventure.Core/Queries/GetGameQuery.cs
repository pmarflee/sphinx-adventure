using Paramore.Darker;
using SphinxAdventure.Core.DTOs;
using System;

namespace SphinxAdventure.Core.Queries
{
    public sealed class GetGameQuery : IQuery<Game>
    {
        public Guid Id { get; }

        public GetGameQuery(Guid id)
        {
            Id = id;
        }
    }
}
