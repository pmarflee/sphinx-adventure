using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using SphinxAdventure.Core.Entities;

namespace SphinxAdventure.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LiteRepository _liteRepository;

        public UserRepository(LiteRepository liteRepository)
        {
            _liteRepository = liteRepository;
        }

        public async Task<User> GetAsync(string username)
        {
            return _liteRepository.SingleOrDefault<User>(user => user.Username == username);
        }

        public async Task InsertAsync(User user)
        {
            _liteRepository.Insert(user);
        }
    }
}
