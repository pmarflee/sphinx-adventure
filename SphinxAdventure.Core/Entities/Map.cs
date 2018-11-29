using SphinxAdventure.Core.Entities.Characteristics;
using SphinxAdventure.Core.Infrastructure.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

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

        public List<ICharacteristic> Characteristics { get; private set; } = new List<ICharacteristic>();

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext ctx)
        {
            if (Characteristics.Count == 0)
            {
                Characteristics.Add(DefaultCharacteristic.Instance);
            }
        }
    }

    public class Item
    {
        public string Description { get; set; }
    }
}
