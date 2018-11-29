using SphinxAdventure.Core.Entities.Exceptions;
using System;
using System.Collections.Generic;

namespace SphinxAdventure.Core.Entities
{
    public class Game : Entity
    {
        public Guid UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public Map Map { get; set; }

        public Location Location { get; set; }

        public List<string> Inventory { get; private set; } = new List<string>();

        internal void Move(string direction)
        {
            Location = Location.GetNextLocation(direction, Map);
        }

        internal void PickUpItem(string item)
        {
            var itemIndex = Location.Items.IndexOf(item);

            if (itemIndex == -1)
            {
                throw new InvalidActionException("Item not present at location");
            }

            Location.Items.RemoveAt(itemIndex);

            Inventory.Add(item);
        }
    }
}
