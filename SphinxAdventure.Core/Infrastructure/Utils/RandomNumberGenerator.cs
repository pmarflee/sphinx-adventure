using System;

namespace SphinxAdventure.Core.Infrastructure.Utils
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private static readonly Random _rnd = new Random();

        public double Next() => _rnd.NextDouble();
    }
}
