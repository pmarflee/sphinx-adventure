using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Infrastructure.Indexes.MappedIndexes;
using YesSql.Indexes;

namespace SphinxAdventure.Core.Infrastructure.Indexes.IndexProviders
{
    public class GameIndexProvider : IndexProvider<Game>
    {
        public override void Describe(DescribeContext<Game> context)
        {
            context.For<GameById>()
                .Map(game => new GameById { EntityId = game.EntityId });

            context.For<GameByUserId>()
                .Map(game => new GameByUserId { UserId = game.UserId });
        }
    }
}
