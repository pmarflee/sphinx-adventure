using System;

namespace SphinxAdventure.Core.Entities.Characteristics
{
    public abstract class LocationCharacteristic
    {
        public abstract bool IsApplicableTo(Game game);

        public void HandleAction(Game game, Action action)
        {
            if (IsApplicableTo(game))
            {
                HandleActionInternal(game, action);
            }
        }

        protected abstract void HandleActionInternal(Game game, Action action);
    }
}
