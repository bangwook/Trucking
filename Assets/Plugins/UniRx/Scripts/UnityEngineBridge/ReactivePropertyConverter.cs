using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace UniRx
{
    /// <summary>
    /// ReactiveProperty를 위한 Newtonsoft.Json JsonConverter
    /// jhlee5 추가
    /// </summary>
    public class ReactivePropertyConverter : JsonConverter
    {
        private const string Value = "Value";       
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var propValue = value.GetType().GetProperty(Value);
            var v = propValue.GetValue(value, null);
            if (propValue.PropertyType.IsClass)
            {
                serializer.Serialize(writer, v);
            }
            else
            {                
                writer.WriteValue(v);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var prop = objectType.GetProperty(Value);
            var argType = prop.PropertyType;

            var value = serializer.Deserialize(reader, argType);
            var inst = Activator.CreateInstance(objectType, value);
            return inst;             

        }

        public override bool CanConvert(Type objectType)
        {
            return false;
        }
        
    }
}