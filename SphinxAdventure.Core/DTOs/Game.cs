using System;

namespace SphinxAdventure.Core.DTOs
{
    public class Game
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public Location Location { get; set; }
    }
}
