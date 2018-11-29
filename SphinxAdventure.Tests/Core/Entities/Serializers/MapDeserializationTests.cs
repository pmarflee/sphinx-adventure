using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Entities.Characteristics;
using System.Linq;
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

        [Fact]
        public void ForestLocationShouldHaveAMazeCharacteristic()
        {
            Assert.Contains(
                Map.Locations["forest"].Characteristics,
                item => item is MazeCharacteristic);
        }

        [Fact]
        public void ForestLocationShouldNotHaveAMazeCharacteristic()
        {
            Assert.DoesNotContain(
                Map.Locations["forest"].Characteristics,
                item => item is DefaultCharacteristic);
        }

        [Fact]
        public void MountainTopLocationShouldHaveADefaultCharacteristic()
        {
            Assert.Contains(
                Map.Locations["mountain_top"].Characteristics,
                item => item is DefaultCharacteristic);
        }

        [Fact]
        public void ShouldDeserializeItems()
        {
            Assert.NotNull(Map.Items);

            Assert.True(Map.Items.TryGetValue("bottle", out _));
            Assert.True(Map.Items.TryGetValue("lamp", out _));
            Assert.True(Map.Items.TryGetValue("keys", out _));
        }
    }
}
