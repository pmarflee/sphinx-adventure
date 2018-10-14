using System;

namespace SphinxAdventure.Core.Entities
{
    public class User : Entity
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
