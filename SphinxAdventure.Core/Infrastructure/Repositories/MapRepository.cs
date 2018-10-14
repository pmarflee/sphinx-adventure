using SphinxAdventure.Core.Entities;
using YesSql;

namespace SphinxAdventure.Core.Infrastructure.Repositories
{
    public class MapRepository : BaseRepository<Map, Indexes.MappedIndexes.MapById>
    {
        protected MapRepository(IStore store) : base(store)
        {
        }
    }
}
