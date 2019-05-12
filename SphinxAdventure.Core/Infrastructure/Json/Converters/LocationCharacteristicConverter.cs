using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SphinxAdventure.Core.Entities.Characteristics;
using System;
using System.Linq;
using System.Reflection;

namespace SphinxAdventure.Core.Infrastructure.Json.Converters
{
    public class LocationCharacteristicConverter : JsonConverter
    {
        private static readonly Type[] _characteristicTypes;
        
        static LocationCharacteristicConverter()
        {
            var characteristicType = typeof(LocationCharacteristic);

            _characteristicTypes = (from type in Assembly.GetExecutingAssembly().GetTypes()
                                where !type.IsAbstract
                                where characteristicType.IsAssignableFrom(type)
                                select type).ToArray();
        }

        public override bool CanRead => true;
        public override bool CanWrite => false;
        public override bool CanConvert(Type objectType) => objectType == typeof(LocationCharacteristic);

        public override object ReadJson(
            JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            (string, bool, JObject) Load()
            {
                if (reader.Value is string stringValue)
                {
                    return (stringValue, false, null);
                }
                else
                {
                    var obj = JObject.Load(reader);
                    return (obj["type"].ToString(), true, obj);
                }
            }

            (var characteristicType, var populate, var jsonObject) = Load();

            var characteristicObjectType = _characteristicTypes.First(
                type => type.Name.StartsWith(characteristicType, 
                StringComparison.InvariantCultureIgnoreCase));
            var characteristic = Activator.CreateInstance(characteristicObjectType);

            if (populate)
            {
                serializer.Populate(jsonObject.CreateReader(), characteristic);
            }

            return characteristic;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException("Use default serialization.");
        }
    }
}
