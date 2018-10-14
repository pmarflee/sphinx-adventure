using Newtonsoft.Json;
using SphinxAdventure.Core.Entities.Converters;
using System.Collections.Generic;

namespace SphinxAdventure.Core.Entities
{
    public class Map : Entity
    {
        public Map() { }

        internal Map(string name, string defaultLocation, Dictionary<string, Location> locations)
        {
            Name = name;
            DefaultLocation = defaultLocation;
            Locations = locations;
        }

        public string Name { get; private set; }

        public string DefaultLocation { get; private set; }

        [JsonConverter(typeof(LocationDictionaryConverter))]
        public IReadOnlyDictionary<string, Location> Locations { get; private set; }
    }

    public class Location
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Transition> Transitions { get; set; } = new List<Transition>();

        public List<Object> Objects { get; set; } = new List<Object>();
    }

    public enum Direction
    {
        North = 1,
        South = 2,
        East = 3,
        West = 4,
        Up = 5,
        Down = 6
    }

    public class Transition
    {
        public Direction Direction { get; set; }

        public string LocationKey { get; set; }
    }

    public class Object
    {
        public string Name { get; set; }
    }
}
