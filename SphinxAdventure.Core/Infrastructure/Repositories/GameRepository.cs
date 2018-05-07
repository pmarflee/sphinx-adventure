using Microsoft.Azure.Documents;
using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Infrastructure;

namespace SphinxAdventure.Core.Infrastructure.Repositories
{
    public class GameRepository : BaseRepository<Game>
    {
        public GameRepository(IDocumentClient documentClient, CosmosDbConfiguration config)
            : base(documentClient, config, "Games")
        {
        }
    }
}
