using System;
using YesSql.Indexes;

namespace SphinxAdventure.Core.Infrastructure.Indexes.MappedIndexes
{
    public abstract class EntityById : MapIndex
    {
        public Guid EntityId { get; set; }
    }
}
