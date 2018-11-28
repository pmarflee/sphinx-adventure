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

        private SphinxAdventure.Core.Entities.Game CreateGame()
        {
            var map = Map.LoadFromResourceFile();

            return new SphinxAdventure.Core.Entities.Game
            {
                UserId = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                Map = map,
                Location = map.Locations.First().Value
            };
        }
    }
}
