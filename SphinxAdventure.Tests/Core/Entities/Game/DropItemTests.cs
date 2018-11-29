using SphinxAdventure.Core.Entities.Exceptions;
using SphinxAdventure.Tests.TestHelpers;
using Xunit;

namespace SphinxAdventure.Tests.Core.Entities.Game
{
    public class DropItemTests
    {
        public SphinxAdventure.Core.Entities.Game Game { get; }

        public DropItemTests()
        {
            Game = GameFactory.Create("raised_path");
            Game.PickUpItem("bottle");
        }

        [Fact]
        public void ShouldRemoveItemFromInventoryIfItemIsPresent()
        {
            Game.DropItem("bottle");

            Assert.DoesNotContain(Game.Inventory, item => item == "bottle");
        }

        [Fact]
        public void ShouldAddItemToLocationIfItemIsPresent()
        {
            Game.DropItem("bottle");

            Assert.Contains(Game.Location.Items, item => item == "bottle");
        }

        [Fact]
        public void ShouldThrowExceptionIfItemIsNotPresentInInventory()
        {
            Assert.Throws<InvalidActionException>(() => Game.DropItem("keys"));
        }

        [Fact]
        public void ShouldNotAddItemToLocationIfItemIsNotPresentInInventory()
        {
            try
            {
                Game.DropItem("keys");
            }
            catch { }

            Assert.DoesNotContain(Game.Location.Items, item => item == "keys");
        }
    }
}
