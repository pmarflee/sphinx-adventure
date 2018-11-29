using Newtonsoft.Json;
using SphinxAdventure.Core.Infrastructure.Json.ContractResolvers;
using SphinxAdventure.Core.Infrastructure.Json.Converters;
using System;
using YesSql;

namespace SphinxAdventure.Core.Infrastructure.Json
{
    public class JsonContentSerializer : IContentSerializer
    {
        private readonly static JsonSerializerSettings _jsonSettings = CreateSettings();

        public object Deserialize(string content, Type type)
        {
            return JsonConvert.DeserializeObject(content, type, _jsonSettings);
        }

        public dynamic DeserializeDynamic(string content)
        {
            return JsonConvert.DeserializeObject<dynamic>(content, _jsonSettings);
        }

        public string Serialize(object item)
        {
            return JsonConvert.SerializeObject(item, _jsonSettings);
        }

        private static JsonSerializerSettings CreateSettings()
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                ContractResolver = new PrivateSetterCamelCasePropertyNamesContractResolver(),
            };
            settings.Converters.Add(new LocationConverter());

            return settings;
        }
    }
}
