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
            Location = Location.GetNextLocation(direction, Map);
        }
    }
}
