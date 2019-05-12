using System;

namespace SphinxAdventure.Core.Entities.Characteristics
{
    public class IsMazeCharacteristic : LocationCharacteristic
    {
        public double Probability { get; set; }

        public override bool IsApplicableTo(Game game) => true;

        protected override void HandleActionInternal(Game game, Action action)
        {
            if (game.NextRandomNumber() > Probability)
            {
                action();
            }
        }
    }
}
