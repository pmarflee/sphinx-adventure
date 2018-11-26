using Newtonsoft.Json;
using SphinxAdventure.Core.Entities;

namespace SphinxAdventure.Core.Maps
{
    public class MapSerializer
    {
        public Map Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<Map>(value);
        }
    }
}
