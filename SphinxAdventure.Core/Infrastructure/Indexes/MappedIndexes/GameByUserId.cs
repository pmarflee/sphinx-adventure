using System;
using YesSql.Indexes;

namespace SphinxAdventure.Core.Infrastructure.Indexes.MappedIndexes
{
    public class GameByUserId : MapIndex
    {
        public Guid UserId { get; set; }
    }
}
