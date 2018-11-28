using System;

namespace SphinxAdventure.Core.Entities
{
    public class Game : Entity
    {
        public Guid UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public Map Map { get; set; }

        public Location Location { get; set; }

        internal void Move(string direction)
        {
            if (!Location.Exits.TryGetValue(direction, out var newLocationKey))
            {
                throw new InvalidOperationException("Invalid direction");
            }

            Location = Map.Locations[newLocationKey];
        }
    }
}
