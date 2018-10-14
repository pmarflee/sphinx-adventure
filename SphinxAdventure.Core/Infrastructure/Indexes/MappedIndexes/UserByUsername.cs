using YesSql.Indexes;

namespace SphinxAdventure.Core.Infrastructure.Indexes.MappedIndexes
{
    public class UserByUsername : MapIndex
    {
        public string Username { get; set; }
    }
}
