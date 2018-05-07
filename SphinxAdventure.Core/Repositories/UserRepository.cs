using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using SphinxAdventure.Core.Infrastructure;

namespace SphinxAdventure.Core.Repositories
{
    public class UserRepository : BaseRepository<Entities.User>, IUserRepository
    {
        public UserRepository(IDocumentClient documentClient, CosmosDbConfiguration config)
            : base(documentClient, config, "Users")
        {
        }

        public async Task<Entities.User> GetAsync(string username)
        {
            var users = await CreateDocumentQuery()
                .Where(user => user.Username == username)
                .ToListAsync();

            return users.FirstOrDefault();
        }
    }
}
