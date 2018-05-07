using System;

namespace SphinxAdventure.Api.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
