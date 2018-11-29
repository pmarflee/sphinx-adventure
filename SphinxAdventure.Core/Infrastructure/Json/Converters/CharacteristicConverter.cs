using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SphinxAdventure.Core.Entities.Characteristics;
using System;
using System.Linq;
using System.Reflection;

namespace SphinxAdventure.Core.Infrastructure.Json.Converters
{
    public class CharacteristicConverter : JsonConverter
    {
        private static readonly Type[] _characteristicTypes;
        
        static CharacteristicConverter()
        {
            var characteristicType = typeof(ICharacteristic);

            _characteristicTypes = (from type in Assembly.GetExecutingAssembly().GetTypes()
                                where !type.IsInterface
                                where characteristicType.IsAssignableFrom(type)
                                select type).ToArray();
        }

        public override bool CanRead => true;
        public override bool CanWrite => false;
        public override bool CanConvert(Type objectType) => objectType == typeof(ICharacteristic);

        public override object ReadJson(
            JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var characteristicType = jsonObject["type"].ToString();
            var characteristicObjectType = _characteristicTypes.First(
                type => type.Name.StartsWith(characteristicType, 
                StringComparison.InvariantCultureIgnoreCase));
            var characteristic = Activator.CreateInstance(characteristicObjectType);

            serializer.Populate(jsonObject.CreateReader(), characteristic);

            return characteristic;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialization.");
        }
    }
}
