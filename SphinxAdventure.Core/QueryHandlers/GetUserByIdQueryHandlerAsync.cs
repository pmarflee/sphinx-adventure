using Paramore.Darker;
using SphinxAdventure.Core.DTOs;
using SphinxAdventure.Core.Infrastructure.Repositories;
using SphinxAdventure.Core.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SphinxAdventure.Core.QueryHandlers
{
    public class GetUserByIdQueryHandlerAsync : QueryHandlerAsync<GetUserByIdQuery, DTOs.User>
    {
        private readonly IRepository<Entities.User> _userRepository;

        public GetUserByIdQueryHandlerAsync(IRepository<Entities.User> userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<DTOs.User> ExecuteAsync(
            GetUserByIdQuery query, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var userEntity = await _userRepository.GetAsync(query.Id);

            return new DTOs.User
            {
                Id = userEntity.EntityId,
                Username = userEntity.Username,
                CreatedOn = userEntity.CreatedOn
            };
        }
    }
}
