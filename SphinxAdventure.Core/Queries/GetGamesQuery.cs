using Paramore.Darker;
using SphinxAdventure.Core.Entities;
using System;
using System.Collections.Generic;

namespace SphinxAdventure.Core.Queries
{
    public sealed class GetGamesQuery : IQuery<IEnumerable<Game>>
    {
        public Guid Id { get; }
        public Guid UserId { get; }

        public GetGamesQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
