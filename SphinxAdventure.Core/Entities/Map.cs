using Newtonsoft.Json;
using SphinxAdventure.Core.Entities.Exceptions;
using SphinxAdventure.Core.Infrastructure.Json;
using SphinxAdventure.Core.Infrastructure.Json.ContractResolvers;
using SphinxAdventure.Core.Infrastructure.Json.Converters;
using SphinxAdventure.Core.Infrastructure.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SphinxAdventure.Core.Entities
{
    public class Map : Entity
    {
        private const string ResourceFileName = "sphinx-adventure.json";

        private static readonly YesSql.IContentSerializer _contentSerializer = 
            new JsonContentSerializer();

        public Dictionary<string, Location> Locations { get; set; }

        public Dictionary<string, Item> Items { get; set; }

        internal static Map LoadFromResourceFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(ResourceFileName));

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return (Map)_contentSerializer.Deserialize(reader.ReadToEnd(), typeof(Map));
                }
            }
        }
    }

    public class Location
    {
        public string Description { get; set; }

        public Dictionary<string, string> Exits { get; set; }

        public List<string> Items { get; set; }

        public Location GetNextLocation(string direction, Map map)
        {
            if (!Exits.TryGetValue(direction, out var newLocationKey))
            {
                throw new InvalidActionException("Invalid direction");
            }

            return GetNextLocationInternal(newLocationKey, map);
        }

        protected virtual Location GetNextLocationInternal(string locationKey, Map map)
        {
            return map.Locations[locationKey];
        }
    }

    public class Maze : Location
    {
        public double Probability { get; set; }
        
        protected override Location GetNextLocationInternal(string locationKey, Map map)
        {
            return Randomizer.NextValue() > Probability 
                ? base.GetNextLocationInternal(locationKey, map) 
                : this;
        }
    }

    public class Item
    {
        public string Description { get; set; }
    }
}
