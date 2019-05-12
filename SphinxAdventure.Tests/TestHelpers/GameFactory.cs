using SphinxAdventure.Core.Entities;
using System;

namespace SphinxAdventure.Tests.TestHelpers
{
    internal static class GameFactory
    {
        public static Game Create(string location = null, Func<double> randomNumberGenerator = null)
        {
            var map = Map.LoadFromResourceFile();

            return new Game(
                Guid.NewGuid(), 
                Guid.NewGuid(), 
                map, 
                DateTime.Now, 
                location, 
                randomNumberGenerator);
        }
    }
}
