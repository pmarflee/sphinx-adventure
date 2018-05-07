using SphinxAdventure.Core.Entities;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetAsync(string username);
    }
}
