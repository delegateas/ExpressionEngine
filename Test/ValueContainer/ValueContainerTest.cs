using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpressionEngine;
using Newtonsoft.Json.Linq;
using NUnit.Framework;using NUnit.Framework.Legacy;
using ValueType = ExpressionEngine.ValueType;

namespace Test
{
    [TestFixture]
    public class ValueContainerTest
    {
        [Test]
        public void TestNormalizeValueContainer()
        {
            var valueContainer = new ValueContainer(new Dictionary<string, ValueContainer>
            {
                {"body/contactid", new ValueContainer("guid")},
                {"body/fullname", new ValueContainer("John Doe")},
                {
                    "body", new ValueContainer(new Dictionary<string, ValueContainer>
                    {
                        {"child/one", new ValueContainer("Child 1")},
                        {"child/two", new ValueContainer("Child 2")}
                    })
                },
                {"body/child/three", new ValueContainer("Child 3")},
            });

            var inner = valueContainer;

            ClassicAssert.AreEqual(1, inner.GetValue<Dictionary<string, ValueContainer>>().Keys.Count);

            var body = inner["body"].GetValue<Dictionary<string, ValueContainer>>();

            ClassicAssert.AreEqual(3, body.Keys.Count);

            ClassicAssert.AreEqual("guid", body["contactid"].GetValue<string>());
            ClassicAssert.AreEqual("John Doe", body["fullname"].GetValue<string>());

            var children = body["child"];

            ClassicAssert.AreEqual(3, children.GetValue<Dictionary<string, ValueContainer>>().Keys.Count);

            ClassicAssert.AreEqual("Child 1", children["one"].GetValue<string>());
            ClassicAssert.AreEqual("Child 2", children["two"].GetValue<string>());
            ClassicAssert.AreEqual("Child 3", children["three"].GetValue<string>());
        }

        [Test]
        public void TestArrayIndexer()
        {
            var valueContainer = new ValueContainer(new List<ValueContainer>
            {
                new ValueContainer("Item 0"),
                new ValueContainer("Item 1"),
                new ValueContainer("Item 2"),
                new ValueContainer("Item 3"),
                new ValueContainer("Item 4"),
                new ValueContainer("Item 5"),
                new ValueContainer("Item 6"),
                new ValueContainer("Item 7"),
                new ValueContainer("Item 8"),
                new ValueContainer("Item 9")
            });

            ClassicAssert.AreEqual("Item 5", valueContainer[5].GetValue<string>());

            var newValue8 = "New item 8";
            valueContainer[8] = new ValueContainer(newValue8);

            ClassicAssert.AreEqual(newValue8, valueContainer[8].GetValue<string>());
        }

        [Test]
        public void TestArrayIndexerFailure()
        {
            var valueContainer = new ValueContainer(150f);

            var exception1 = ClassicAssert.Throws<InvalidOperationException>(() =>
            {
                var temp = valueContainer[0];
            });
            ClassicAssert.AreEqual(exception1.Message, "Index operations can only be performed on arrays.");

            var exception2 = ClassicAssert.Throws<InvalidOperationException>(() =>
            {
                valueContainer[0] = new ValueContainer(100f);
            });
            ClassicAssert.AreEqual(exception2.Message, "Index operations can only be performed on arrays.");
        }

        [Test]
        public void TestObjectIndexer()
        {
            var valueContainer = new ValueContainer(new Dictionary<string, ValueContainer>
            {
                {
                    "body", new ValueContainer(new Dictionary<string, ValueContainer>
                    {
                        {"name", new ValueContainer("James P. \"Sulley\" Sullivan")}
                    })
                }
            });

            ClassicAssert.AreEqual(ValueType.Object, valueContainer["body"].Type());

            ClassicAssert.AreEqual("James P. \"Sulley\" Sullivan", valueContainer["body"]["name"].GetValue<string>());

            var guidStr = Guid.NewGuid().ToString();
            valueContainer["body"]["id"] = new ValueContainer(guidStr);

            ClassicAssert.AreEqual(guidStr, valueContainer["body"]["id"].GetValue<string>());
        }

        [Test]
        public void TestObjectIndexerFailure()
        {
            var valueContainer = new ValueContainer();

            var exception1 = ClassicAssert.Throws<InvalidOperationException>(() =>
            {
                var temp = valueContainer["name"];
            });
            ClassicAssert.AreEqual("Index operations can only be performed on objects.", exception1.Message);


            var exception2 = ClassicAssert.Throws<InvalidOperationException>(() =>
            {
                valueContainer["name"] = new ValueContainer("Mendel Stromm");
            });

            ClassicAssert.AreEqual("Index operations can only be performed on objects.", exception2.Message);
        }

        [Test]
        public void TestObjectIndexerKeyPath()
        {
            var valueContainer = new ValueContainer(new Dictionary<string, ValueContainer>());

            valueContainer["body/name"] = new ValueContainer("John Doe");

            ClassicAssert.AreEqual("John Doe", valueContainer["body/name"].GetValue<string>());
        }
        
        [Test]
        public async Task TestJValueToValueContainer()
        {
            var expectedValue = "Value";
            var jValue = new JValue(expectedValue);

            var valueContainer = await ValueContainerExtension.CreateValueContainerFromJToken(jValue);

            ClassicAssert.IsNotNull(valueContainer);
            ClassicAssert.AreEqual(ValueType.String, valueContainer.Type());
            ClassicAssert.AreEqual(expectedValue, valueContainer.GetValue<string>());
        }

        public T GetValue<T>(ValueContainer valueContainer)
        {
            return valueContainer.GetValue<T>();
        }

        [Test]
        public async Task TestJsonToValueContainerSimple()
        {
            var json = JToken.Parse("{\"Terminate\":{\"Key1\":\"Value1\",\"Key2\":\"Value2\"}}");
            
            var valueContainer = await ValueContainerExtension.CreateValueContainerFromJToken(json);

            ClassicAssert.IsTrue(valueContainer.GetValue<Dictionary<string, ValueContainer>>().ContainsKey("Terminate"));
            ClassicAssert.AreEqual(1,valueContainer.GetValue<Dictionary<string, ValueContainer>>().Count);
            
            ClassicAssert.AreEqual(2, valueContainer["Terminate"].GetValue<Dictionary<string,ValueContainer>>().Count);
        }
        
        [Test]
        public async Task TestJsonToValueContainerTypicalInputSection()
        {
            var json = JToken.Parse(
                "{\"inputs\": {\"host\": {\"connectionName\": \"shared_commondataserviceforapps\",\"operationId\": \"GetItem\"," +
                "\"apiId\": \"/providers/Microsoft.PowerApps/apis/shared_commondataserviceforapps\"}," +
                "\"parameters\": {\"entityName\": \"accounts\",\"recordId\": \"@triggerOutputs()?['body/accountid']\"}," +
                "\"authentication\": \"@parameters('$authentication')\"}}");
            
            var valueContainer = await ValueContainerExtension.CreateValueContainerFromJToken(json);
            
            ClassicAssert.IsTrue(valueContainer.GetValue<Dictionary<string, ValueContainer>>().ContainsKey("inputs"));
            ClassicAssert.AreEqual(1,valueContainer.GetValue<Dictionary<string, ValueContainer>>().Count);

            var inputs = valueContainer["inputs"];
            var inputsDict= valueContainer["inputs"].GetValue<Dictionary<string, ValueContainer>>();
            
            ClassicAssert.IsTrue(inputsDict.ContainsKey("host"));
            ClassicAssert.IsTrue(inputsDict.ContainsKey("parameters"));
            ClassicAssert.IsTrue(inputsDict.ContainsKey("authentication"));

            ClassicAssert.AreEqual(3,inputs.GetValue<Dictionary<string,ValueContainer>>().Count);

            var host = inputs["host"];
            ClassicAssert.AreEqual(3,host.GetValue<Dictionary<string,ValueContainer>>().Count);
            
            var parameters = inputs["parameters"];
            ClassicAssert.AreEqual(2,parameters.GetValue<Dictionary<string,ValueContainer>>().Count);
        }
    }
}