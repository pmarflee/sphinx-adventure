using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Infrastructure.Indexes.MappedIndexes;
using YesSql.Indexes;

namespace SphinxAdventure.Core.Infrastructure.Indexes.IndexProviders
{
    public class UserIndexProvider : IndexProvider<User>
    {
        public override void Describe(DescribeContext<User> context)
        {
            context.For<UserByUsername>()
                .Map(user => new UserByUsername { Username = user.Username });

            context.For<UserById>()
                .Map(user => new UserById { EntityId = user.EntityId });
        }
    }
}
