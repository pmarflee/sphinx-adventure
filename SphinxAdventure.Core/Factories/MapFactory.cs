using SphinxAdventure.Core.Entities;

namespace SphinxAdventure.Core.Factories
{
    public class MapProps
    {

    }

    public class MapFactory : IFactory<Map, MapProps>
    {
        public Map Create(MapProps props)
        {
            return Map.LoadFromResourceFile();
        }
    }
}
