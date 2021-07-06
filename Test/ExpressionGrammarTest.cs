using System.Collections.Generic;
using ExpressionEngine;
using ExpressionEngine.Functions.Base;
using NUnit.Framework;

namespace Test
{
    public class ExpressionGrammarTest
    {
        private ExpressionGrammar _expressionGrammar;
        private DummyFunction _dummyFunction;

        [SetUp]
        public void SetUp()
        {
            _dummyFunction = new DummyFunction(new ValueContainer(new Dictionary<string, ValueContainer>
            {
                {
                    "prop1", new ValueContainer(new Dictionary<string, ValueContainer>
                    {
                        {
                            "prop2", new ValueContainer(new Dictionary<string, ValueContainer>
                            {
                                {"prop3", new ValueContainer("value")}
                            })
                        }
                    })
                }
            }));

            var functions = new List<IFunction>
            {
                _dummyFunction
            };

            _expressionGrammar = new ExpressionGrammar(functions);
        }

        [Test]
        public void IndicesTest()
        {
            const string expressionString = "@dummyFunction()['prop1'].prop2['prop3']";

            var res = _expressionGrammar.EvaluateToString(expressionString);

            Assert.AreEqual("value", res);
        }

        [Test]
        public void BooleanTest()
        {
            const string expressionString = "@dummyFunction(true, false)";

            _expressionGrammar.EvaluateToValueContainer(expressionString);

            Assert.NotNull(_dummyFunction._parameters);
            Assert.AreEqual(2, _dummyFunction._parameters.Length);
            var param1 = _dummyFunction._parameters[0];
            var param2 = _dummyFunction._parameters[1];
            Assert.NotNull(param1);
            Assert.NotNull(param2);
            Assert.AreEqual(ValueContainer.ValueType.Boolean, param1.Type());
            Assert.AreEqual(ValueContainer.ValueType.Boolean, param2.Type());
            Assert.AreEqual(true, param1.GetValue<bool>());
            Assert.AreEqual(false, param2.GetValue<bool>());
        }
    }

    public class DummyFunction : Function
    {
        private readonly ValueContainer _valueContainer;
        internal ValueContainer[] _parameters;

        public DummyFunction() : base("dummyFunction")
        {
        }

        public DummyFunction(ValueContainer valueContainer) : base("dummyFunction")
        {
            _valueContainer = valueContainer;
        }

        public override ValueContainer ExecuteFunction(params ValueContainer[] parameters)
        {
            _parameters = parameters;
            return _valueContainer;
        }
    }
}