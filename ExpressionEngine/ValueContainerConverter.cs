using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ExpressionEngine
{
    public class ValueContainerConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(value is ValueContainer data)
            {
                switch (data.Type())
                {
                    case ValueType.Boolean:
                        serializer.Serialize(writer, data.GetValue<bool>());
                       //writer.WriteValue(data.GetValue<bool>());
                        break;
                    case ValueType.Integer:
                        serializer.Serialize(writer, data.GetValue<int>());
                        //writer.WriteValue(data.GetValue<int>());
                        break;
                    case ValueType.Float:
                        serializer.Serialize(writer, data.GetValue<float>());
                       // writer.WriteValue(data.GetValue<float>());
                        break;
                    case ValueType.String:
                        serializer.Serialize(writer, data.GetValue<string>());
                       // writer.WriteValue(data.GetValue<string>());
                        break;
                    case ValueType.Object:
                        serializer.Serialize(writer, data.AsDict());
                        //writer.WriteStartObject();

                        //foreach (var prop in data.AsDict())
                        //{
                        //    writer.WritePropertyName(prop.Key);
                        //    writer.WriteValue(prop.Value);
                        //}

                        //writer.WriteEndObject();
                        break;
                    case ValueType.Array:
                        serializer.Serialize(writer, data.GetValue<List<ValueContainer>>());
                        //writer.WriteStartArray();

                        //foreach (var prop in data.GetValue<List<ValueContainer>>())
                        //{   
                        //    writer.WriteValue(prop);
                        //}

                        //writer.WriteEndArray();
                        break;
                    case ValueType.Null:
                        serializer.Serialize(writer, null);
                       // writer.WriteNull();
                        break;
                    case ValueType.Date:
                        serializer.Serialize(writer, data.GetValue<DateTimeOffset>());
                      //  writer.WriteValue(data.GetValue<DateTimeOffset>());
                        break;
                    default:
                        break;
                }
                
            }
           
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return new ValueContainer((string) reader.Value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ValueContainer);
        }
    }
}