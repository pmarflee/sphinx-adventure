using SphinxAdventure.Core.Entities.Exceptions;
using SphinxAdventure.Tests.TestHelpers;
using Xunit;

namespace SphinxAdventure.Tests.Core.Entities.Game
{
    public class PickUpItemTests
    {
        public SphinxAdventure.Core.Entities.Game Game { get; }

        public PickUpItemTests()
        {
            Game = GameFactory.Create("raised_path");
        }

        [Fact]
        public void ShouldAddItemToInventoryIfItemIsPresentAtLocation()
        {
            Game.PickUpItem("bottle");

            Assert.Contains(Game.Inventory, item => item == "bottle");
        }

        [Fact]
        public void ShouldThrowInvalidActionExceptionIfItemIsNotPresentAtLocation()
        {
            Assert.Throws<InvalidActionException>(() => Game.PickUpItem("keys"));
        }

        [Fact]
        public void ShouldNotAddItemToInventoryIfItemIsNotPresentAtLocation()
        {
            try
            {
                Game.PickUpItem("keys");
            }
            catch { }

            Assert.DoesNotContain(Game.Inventory, item => item == "keys");
        }

        [Fact]
        public void ShouldRemoveItemFromLocationWhenItemIsPickedUp()
        {
            Game.PickUpItem("bottle");

            Assert.DoesNotContain(Game.Location.Items, item => item == "bottle");
        }
    }
}
