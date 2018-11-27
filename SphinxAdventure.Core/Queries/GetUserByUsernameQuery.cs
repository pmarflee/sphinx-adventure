using Paramore.Darker;

namespace SphinxAdventure.Core.Queries
{
    public sealed class GetUserByUsernameQuery : IQuery<DTOs.User>
    {
        public string Username { get; }

        public GetUserByUsernameQuery(string username)
        {
            Username = username;
        }
    }
}
