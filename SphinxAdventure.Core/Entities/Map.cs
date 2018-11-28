using Newtonsoft.Json;
using SphinxAdventure.Core.Infrastructure.Json.Converters;
using SphinxAdventure.Core.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SphinxAdventure.Core.Entities
{
    public class Map : Entity
    {
        private const string ResourceFileName = "sphinx-adventure.json";

        public Dictionary<string, Location> Locations { get; set; }

        internal static Map LoadFromResourceFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(ResourceFileName));

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return JsonConvert.DeserializeObject<Map>(
                        reader.ReadToEnd(),
                        new LocationConverter());
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
                throw new InvalidOperationException("Invalid direction");
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
}
