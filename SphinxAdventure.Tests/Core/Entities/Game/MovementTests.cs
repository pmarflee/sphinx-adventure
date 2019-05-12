using SphinxAdventure.Core.Entities.Exceptions;
using SphinxAdventure.Tests.TestHelpers;
using Xunit;

namespace SphinxAdventure.Tests.Core.Entities.Game
{
    public class MovementTests
    {
        public MovementTests()
        {
            Game = GameFactory.Create();
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
            Assert.Throws<InvalidActionException>(() => Game.Move("d"));
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
        public void ShouldRemainInCurrentLocationWhenCurrentLocationIsAMazeAndRandomNumberDoesNotExceedProbability()
        {
            var game = GameFactory.Create("forest", () => 0.5);
            var originalLocation = game.Location;

            game.Move("n");

            Assert.Equal(originalLocation, game.Location);
        }

        [Fact]
        public void ShouldNotRemainInCurrentLocationWhenCurrentLocationIsAMazeAndRandomNumberDoesExceedProbability()
        {
            var game = GameFactory.Create("forest", () => 0.9);
            var originalLocation = game.Location;

            game.Move("n");

            Assert.NotEqual(originalLocation, game.Location);
        }
    }
}
