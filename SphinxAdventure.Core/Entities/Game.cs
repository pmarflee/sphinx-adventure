using System;

namespace SphinxAdventure.Core.Entities
{
    public class Game : Entity
    {
        public Guid UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public Map Map { get; set; }

        public Location Location { get; set; }

        internal void MoveToLocation(string locationKey)
        {
            if (!Map.Locations.TryGetValue(locationKey, out var location))
            {
                throw new InvalidOperationException("Cannot move to location");
            }

            Location = location;
        }
    }
}
