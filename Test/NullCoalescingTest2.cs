using System.Collections.Generic;
using System.Threading.Tasks;
using ExpressionEngine;
using ExpressionEngine.Functions.Base;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class NullCoalescingTest2
    {
        private ExpressionGrammar _expressionGrammar;
        private ReturnData _returnData;

        [SetUp]
        public void Setup()
        {
            _returnData = new ReturnData();

            var functions = new List<IFunction> {_returnData};

            _expressionGrammar = new ExpressionGrammar(functions, null);
        }

        [Test]
        public async Task SimpleTest()
        {
            _returnData.ValueContainer = new ValueContainer();

            var input = "@returnData()?.doc?.name";

            var result = await _expressionGrammar.EvaluateToValueContainer(input);
            
            Assert.AreEqual(new ValueContainer(), result);
        }
    }

    internal class ReturnData : Function
    {
        internal ValueContainer ValueContainer;
        internal ValueContainer[] Parameters;

        public ReturnData() : base("returnData")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            Parameters = parameters;
            return new ValueTask<ValueContainer>(ValueContainer);
        }
    }
}