using Microsoft.Azure.Documents;
using SphinxAdventure.Core.Entities;

namespace SphinxAdventure.Core.Infrastructure.Repositories
{
    public class MapRepository : BaseRepository<Map>
    {
        protected MapRepository(
            IDocumentClient documentClient, 
            CosmosDbConfiguration config) : base(documentClient, config, "Maps")
        {
        }
    }
}
