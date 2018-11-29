using SphinxAdventure.Core.Entities;
using System;
using System.Linq;

namespace SphinxAdventure.Tests.TestHelpers
{
    internal static class GameFactory
    {
        public static Game Create(string location = null)
        {
            var map = Map.LoadFromResourceFile();

            return new Game
            {
                UserId = Guid.NewGuid(),
                CreatedOn = DateTime.Now,
                Map = map,
                Location = location != null ? map.Locations[location] : map.Locations.First().Value
            };
        }
    }
}
