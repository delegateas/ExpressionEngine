using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpressionEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using ValueType = ExpressionEngine.ValueType;

namespace Test
{
    [TestFixture]
    public class ValueContainerJTokenConstructorTest
    {
        static DateTimeOffset now = DateTimeOffset.UtcNow;

        [TestCaseSource(nameof(_valueContainerConstructorInput))]
        public async Task ConstructorTest(JToken jToken, ValueType expectedValueType,
            ValueContainer expectedValue)
        {

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                DateParseHandling = Newtonsoft.Json.DateParseHandling.DateTimeOffset,
            };

           // var valueContainer1 = await ValueContainerExtension.CreateValueContainerFromJToken(JsonConvert.DeserializeObject<JObject>("{\"test\":\"2022-02-01T10:14:00Z\"}"));
            //var valueContainer2 = await ValueContainerExtension.CreateValueContainerFromJToken(JToken.FromObject(new { test = DateTime.UtcNow}, JsonSerializer.CreateDefault(new JsonSerializerSettings
            //{
            //    DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
            //    DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
            //    DateParseHandling = Newtonsoft.Json.DateParseHandling.DateTimeOffset,
            //})));

            var valueContainer = await ValueContainerExtension.CreateValueContainerFromJToken(jToken);
            
            Assert.AreEqual(expectedValueType, valueContainer.Type());
            Assert.AreEqual(expectedValue, valueContainer);
        }
        
        private static object[] _valueContainerConstructorInput =
        {
           
           
            new object[]
            {
                new JObject(new JProperty("value",new JArray(new JObject(new JProperty("id",Guid.Empty))))),
                ValueType.Object,
                new ValueContainer(
                    new Dictionary<string,ValueContainer>{
                        ["value"] = new ValueContainer(new ValueContainer[]{
                            new ValueContainer(new Dictionary<string, ValueContainer> { ["id"]= new ValueContainer(Guid.Empty) })
                        })
                        }
                    )
            },
            new object[]
            {
                new JValue("Some random string"),
                ValueType.String,
                new ValueContainer("Some random string")
            },
            new object[]
            {
                new JValue(23),
                ValueType.Integer,
                new ValueContainer(23)
            },
            new object[]
            {
                new JValue(25.6),
                ValueType.Float,
                new ValueContainer(25.6)
            },
            new object[]
            {
                new JValue(true),
                ValueType.Boolean,
                new ValueContainer(true)
            },
            new object[]
            {
                new JValue(new Guid("b4a9b9ee-96c3-49c4-871c-bc74870a134a")),
                ValueType.Guid,
                new ValueContainer(new Guid("b4a9b9ee-96c3-49c4-871c-bc74870a134a"))
            },
            new object[]
            {
                new JValue((object) null),
                ValueType.Null,
                new ValueContainer()
            },
            new object[]
            {
                new JValue(true),
                ValueType.Boolean,
                new ValueContainer(true)
            },
            // new object[]
            //{
            //    new JValue(now),
            //    ValueType.Date,
            //    new ValueContainer(now)
            //}
        };
    }
}