using Newtonsoft.Json;
using SphinxAdventure.Core.Entities.Exceptions;
using SphinxAdventure.Core.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SphinxAdventure.Core.Entities
{
    public class Game : Entity
    {
        [JsonConstructor]
        public Game()
        {

        }

        public Game(Guid entityId, Guid userId, Map map, DateTime createdOn, 
            string location = null, Func<double> nextRandomNumber = null)
        {
            EntityId = entityId;
            UserId = userId;
            Map = map;
            CreatedOn = createdOn;
            Location = !string.IsNullOrEmpty(location)
                ? map.Locations[location] 
                : map.Locations.Values.FirstOrDefault();
            if (nextRandomNumber != null)
            {
                NextRandomNumber = nextRandomNumber;
            }
        }

        public Guid UserId { get; }

        public DateTime CreatedOn { get; }

        public Map Map { get; }

        public Location Location { get; private set; }

        public Dictionary<string, Item> Items { get; private set; } = new Dictionary<string, Item>();

        public List<string> Inventory { get; private set; } = new List<string>();

        public bool InProgress { get; internal set; }

        public bool PlayerKilled { get; internal set; }

        public Func<double> NextRandomNumber { get; internal set; }

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
                characteristic.HandleAction(this, action);
            }
        }
    }
}
