using System;

namespace SphinxAdventure.Core.Infrastructure.Utils
{
    public static class Randomizer
    {
        private static Random _rnd = new Random();

        public static double NextValue()
        {
            return _rnd.NextDouble();
        }
    }
}
