using System;

namespace SphinxAdventure.Core.Entities.Characteristics
{
    public class DefaultCharacteristic : ICharacteristic
    {
        public string Key => "default";

        public void Handle(Game game, Action action)
        {
            action();
        }

        public static readonly DefaultCharacteristic Instance = new DefaultCharacteristic();
    }
}
