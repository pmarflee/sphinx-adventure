using SphinxAdventure.Core.Entities;
using Xunit;

namespace SphinxAdventure.Tests.Core.Entities.Serializers
{
    public class MapDeserializationTests
    {
        public MapDeserializationTests()
        {
            Map = Map.LoadFromResourceFile();
        }

        private Map Map { get; }

        [Fact]
        public void ShouldDeserializeMapWithoutError()
        {
        }

        [Fact]
        public void ShouldDeserializeDefaultLocation()
        {
            Assert.True(Map.Locations.TryGetValue("mountain_top", out var location));

            Assert.Equal(4, location.Exits.Count);

            Assert.Contains(location.Exits, exit => exit.Key == "n" && exit.Value == "raised_path");
            Assert.Contains(location.Exits, exit => exit.Key == "s" && exit.Value == "forest");
            Assert.Contains(location.Exits, exit => exit.Key == "w" && exit.Value == "forest");
            Assert.Contains(location.Exits, exit => exit.Key == "e" && exit.Value == "forest");

            Assert.Empty(location.Items);
        }
    }
}
