using Paramore.Darker;
using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Queries;
using SphinxAdventure.Core.Infrastructure.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.QueryHandlers
{
    public class GetUserQueryHandler : QueryHandlerAsync<GetUserQuery, User>
    {
        private readonly IRepository<User> _userRepository;

        public GetUserQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<User> ExecuteAsync(
            GetUserQuery query, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _userRepository.GetByUsernameAsync(query.Username);
        }
    }
}
