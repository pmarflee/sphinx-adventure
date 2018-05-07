using Paramore.Darker;
using SphinxAdventure.Core.Entities;

namespace SphinxAdventure.Core.Queries
{
    public sealed class GetUserQuery : IQuery<User>
    {
        public string Username { get; }

        public GetUserQuery(string username)
        {
            Username = username;
        }
    }
}
