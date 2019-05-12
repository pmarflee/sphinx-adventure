using System;

namespace SphinxAdventure.Core.Entities.Characteristics
{
    public class DefaultCharacteristic : LocationCharacteristic
    {
        public override bool IsApplicableTo(Game game) => true;

        protected override void HandleActionInternal(Game game, Action action) => action();

        public static readonly DefaultCharacteristic Instance = new DefaultCharacteristic();
    }
}
