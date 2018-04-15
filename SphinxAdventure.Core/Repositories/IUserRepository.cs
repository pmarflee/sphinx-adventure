using SphinxAdventure.Core.Entities;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string username);
        Task InsertAsync(User user);
    }
}
