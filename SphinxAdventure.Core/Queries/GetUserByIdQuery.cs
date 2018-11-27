using Paramore.Darker;
using System;

namespace SphinxAdventure.Core.Queries
{
    public class GetUserByIdQuery : IQuery<DTOs.User>
    {
        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
