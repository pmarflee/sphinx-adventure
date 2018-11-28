using System;
using System.Linq;
using SphinxAdventure.Core.Entities;
using Xunit;

namespace SphinxAdventure.Tests.Core.Entities.Game
{
    public class MovementTests
    {
        public MovementTests()
        {
            Game = CreateGame();
        }

        public SphinxAdventure.Core.Entities.Game Game { get; }

        [Fact]
        public void ShouldUpdateLocationIfDirectionIsValid()
        {
            Game.Move("n");

            Assert.Equal(Game.Map.Locations["raised_path"], Game.Location);
        }

        [Fact]
        public void ShouldThrowInvalidOperationExceptionIfDirectionIsInvalid()
        {
            Assert.Throws<InvalidOperationException>(() => Game.Move("d"));
        }

        [Fact]
        public void ShouldNotUpdateLocationIfDirectionIsInvalid()
        {
            var originalLocation = Game.Location;

            try
            {
                Game.Move("d");
            }
            catch { }

            Assert.Equal(originalLocation, Game.Location);
        }

        [Fact]
        public void ShouldNotAlwaysMoveWhenCurrentLocationIsAMaze()
        {
            (Location, Location) Locations()
            {
                var game = CreateGame("forest");
                var originalLocation = game.Location;

                game.Move("n");

                return (originalLocation, game.Location);
            }

            var locations = Enumerable.Range(0, 100).Select(i => Locations());

            Assert.Contains(locations, pair => pair.Item1 == pair.Item2);
        }

        [Fact]
        public void ShouldNotAlwaysRemainInCurrentLocationWhenCurrentLocationIsAMaze()
        {
            (Location, Location) Locations()
            {
                var game = CreateGame("forest");
                var originalLocation = game.Location;

                game.Move("n");

                return (originalLocation, game.Location);
            }

            var locations = Enumerable.Range(0, 100).Select(i => Locations());

            Assert.Contains(locations, pair => pair.Item1 != pair.Item2);
        }

        private SphinxAdventure.Core.Entities.Game CreateGame(string location = null)
        {
            var map = Map.LoadFromResourceFile();

            return new SphinxAdventure.Core.Entities.Game
            {
                UserId = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                Map = map,
                Location = location != null ? map.Locations[location] : map.Locations.First().Value
            };
        }
    }
}
