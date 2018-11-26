using SphinxAdventure.Core.Entities;

namespace SphinxAdventure.Core.Factories
{
    public class MapFactory : IFactory<Map>
    {
        public Map Create()
        {
            return Map.LoadFromResourceFile();
        }
    }
}
