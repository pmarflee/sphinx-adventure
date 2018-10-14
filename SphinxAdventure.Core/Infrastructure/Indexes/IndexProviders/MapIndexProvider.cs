using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Infrastructure.Indexes.MappedIndexes;
using YesSql.Indexes;

namespace SphinxAdventure.Core.Infrastructure.Indexes.IndexProviders
{
    public class MapIndexProvider : IndexProvider<Map>
    {
        public override void Describe(DescribeContext<Map> context)
        {
            context.For<MapById>()
                .Map(map => new MapById { EntityId = map.EntityId });
        }
    }
}
