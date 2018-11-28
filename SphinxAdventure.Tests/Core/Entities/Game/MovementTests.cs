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
        public void ShouldUpdateLocationIfExitIsAvailable()
        {
            Game.MoveToLocation("raised_path");

            Assert.Equal(Game.Map.Locations["raised_path"], Game.Location);
        }

        [Fact]
        public void ShouldThrowInvalidOperationExceptionIfExitIsNotAvailable()
        {
            Assert.Throws<InvalidOperationException>(() => Game.MoveToLocation("end_of_road"));
        }

        [Fact]
        public void ShouldNotUpdateLocationIfExitIsNotAvailable()
        {
            var originalLocation = Game.Location;

            try
            {
                Game.MoveToLocation("end_of_road");
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
