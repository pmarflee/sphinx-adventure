using JsonNet.PrivateSettersContractResolvers;
using Newtonsoft.Json;
using SphinxAdventure.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SphinxAdventure.Tests.Core.Entities.Serializers
{
    public class MapSerializerTests
    {
        [Fact]
        public void MapShouldSerializeAndDeserializeCorrectly()
        {
            var map = new Map(
                "Sphinx Adventure", 
                "Road",
                new Dictionary<string, Location>
                {
                    ["Start"] = new Location { Name = "Start", Description = "This is the start" }
                });

            var result = JsonConvert.SerializeObject(map);

            var map2 = JsonConvert.DeserializeObject<Map>(result, 
                new JsonSerializerSettings
                {
                    ContractResolver = new PrivateSetterCamelCasePropertyNamesContractResolver()
                });

            Assert.Equal(map.Name, map2.Name);
            Assert.Equal(map.DefaultLocation, map2.DefaultLocation);
            Assert.Equal(map.Locations.Count, map2.Locations.Count);
            Assert.Equal(map.Locations.Keys.First(), map2.Locations.Keys.First());
            Assert.Equal(map.Locations["Start"].Name, map2.Locations["Start"].Name);
            Assert.Equal(map.Locations["Start"].Description, map2.Locations["Start"].Description);
        }
    }
}
