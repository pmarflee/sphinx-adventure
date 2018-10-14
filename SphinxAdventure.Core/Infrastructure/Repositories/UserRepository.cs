using SphinxAdventure.Core.Infrastructure.Indexes.MappedIndexes;
using System.Threading.Tasks;
using YesSql;

namespace SphinxAdventure.Core.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<Entities.User, UserById>
    {
        public UserRepository(IStore store) : base(store)
        {
        }
    }

    public static class UserRepositoryExtensions
    {
        public static async Task<Entities.User> GetByUsernameAsync(
            this IRepository<Entities.User> repository, string username)
        {
            using (var session = repository.CreateSession())
            {
                return await session
                    .Query<Entities.User, UserByUsername>(index => index.Username == username)
                    .FirstOrDefaultAsync();
            }
        }
    }
}
