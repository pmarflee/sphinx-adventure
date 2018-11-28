using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SphinxAdventure.Core.Entities;
using System;
using System.Linq;

namespace SphinxAdventure.Core.Infrastructure.Json.Converters
{
    public class LocationConverter : JsonConverter
    {
        private static readonly string[] ProbabilityKeys = { "probability", "Probability" };

        public override bool CanRead => true;
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Location);
        }

        public override object ReadJson(
            JsonReader reader, Type objectType, 
            object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var location = ProbabilityKeys.Any(jsonObject.ContainsKey)
                ? new Maze() : new Location();

            serializer.Populate(jsonObject.CreateReader(), location);

            return location;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialization.");
        }
    }
}
