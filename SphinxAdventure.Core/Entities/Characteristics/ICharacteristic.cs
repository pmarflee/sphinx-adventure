using System;

namespace SphinxAdventure.Core.Entities.Characteristics
{
    public interface ICharacteristic
    {
        string Key { get; }

        void Handle(Game game, Action action);
    }
}
