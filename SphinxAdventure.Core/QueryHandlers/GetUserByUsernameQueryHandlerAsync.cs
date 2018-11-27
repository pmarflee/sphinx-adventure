using Paramore.Darker;
using SphinxAdventure.Core.Queries;
using SphinxAdventure.Core.Infrastructure.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.QueryHandlers
{
    public class GetUserByUsernameQueryHandlerAsync : QueryHandlerAsync<GetUserByUsernameQuery, DTOs.User>
    {
        private readonly IRepository<Entities.User> _userRepository;

        public GetUserByUsernameQueryHandlerAsync(IRepository<Entities.User> userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<DTOs.User> ExecuteAsync(
            GetUserByUsernameQuery query, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var userEntity = await _userRepository.GetByUsernameAsync(query.Username);

            return new DTOs.User
            {
                Id = userEntity.EntityId,
                Username = userEntity.Username,
                CreatedOn = userEntity.CreatedOn
            };
        }
    }
}
