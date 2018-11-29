using SphinxAdventure.Core.Infrastructure.Utils;
using System;

namespace SphinxAdventure.Core.Entities.Characteristics
{
    public class MazeCharacteristic : ICharacteristic
    {
        public string Key => "maze";

        public double Probability { get; set; }

        public void Handle(Game game, Action action)
        {
            if (Randomizer.NextValue() > Probability)
            {
                action();
            }
        }
    }
}
