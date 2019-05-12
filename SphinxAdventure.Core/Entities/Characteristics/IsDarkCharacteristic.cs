using System;

namespace SphinxAdventure.Core.Entities.Characteristics
{
    public class IsDarkCharacteristic : LocationCharacteristic
    {
        public double Probability { get; set; }

        public override bool IsApplicableTo(Game game)
        {
            return !game.Inventory.Contains("lamp") 
                || !game.Items["lamp"].HasCharacteristic("islit");
        }

        protected override void HandleActionInternal(Game game, Action action)
        {
            if (game.NextRandomNumber() < Probability)
            {
                game.InProgress = false;
                game.PlayerKilled = true;
            }
        }
    }
}
