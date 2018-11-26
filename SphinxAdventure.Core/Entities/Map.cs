using Newtonsoft.Json;
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
                    return JsonConvert.DeserializeObject<Map>(reader.ReadToEnd());
                }
            }
        }
    }

    public class Location
    {
        public string Description { get; set; }

        public Dictionary<string, string> Exits { get; set; }

        public List<string> Items { get; set; }
    }
}
