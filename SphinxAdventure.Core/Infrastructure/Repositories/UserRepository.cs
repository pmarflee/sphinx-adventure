using Microsoft.Azure.Documents;
using SphinxAdventure.Core.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<Entities.User>
    {
        public UserRepository(IDocumentClient documentClient, CosmosDbConfiguration config)
            : base(documentClient, config, "Users")
        {
        }
    }

    public static class UserRepositoryExtensions
    {
        public static async Task<Entities.User> GetByUsernameAsync(
            this IRepository<Entities.User> repository, string username)
        {
            return (await repository.CreateQuery()
                .Where(user => user.Username == username)
                .ToListAsync())
                .FirstOrDefault();
        }
    }
}
