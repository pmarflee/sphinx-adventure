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
            if (!Location.Exits.TryGetValue(direction, out var newLocationKey))
            {
                throw new InvalidActionException("Invalid direction");
            }

            ApplyCharacteristics(() => Location = Map.Locations[newLocationKey]);
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

        internal void DropItem(string item)
        {
            var itemIndex = Inventory.IndexOf(item);

            if (itemIndex == -1)
            {
                throw new InvalidActionException("Item not present in inventory");
            }

            Inventory.RemoveAt(itemIndex);

            Location.Items.Add(item);
        }

        private void ApplyCharacteristics(Action action)
        {
            foreach (var characteristic in Location.Characteristics)
            {
                characteristic.Handle(this, action);
            }
        }
    }
}
