using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SphinxAdventure.Core.Entities.Converters
{
    internal class LocationDictionaryConverter : JsonConverter<IReadOnlyDictionary<string, Location>>
    {
        public override IReadOnlyDictionary<string, Location> ReadJson(JsonReader reader, Type objectType, IReadOnlyDictionary<string, Location> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return JObject.ReadFrom(reader)
                .ToObject<List<Location>>()
                .ToDictionary(l => l.Name, l => l);
        }

        public override void WriteJson(JsonWriter writer, IReadOnlyDictionary<string, Location> value, JsonSerializer serializer)
        {
            writer.WriteStartArray();

            foreach (var location in value.Values)
            {
                JObject.FromObject(location).WriteTo(writer);
            }

            writer.WriteEndArray();
        }
    }
}
