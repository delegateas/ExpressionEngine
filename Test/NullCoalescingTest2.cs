using System.Collections.Generic;
using System.Threading.Tasks;
using ExpressionEngine;
using ExpressionEngine.Functions.Base;
using Microsoft.Extensions.DependencyInjection;
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
            var t = new ServiceCollection();
            t.RegisterTransientFunctionAlias<DummyFunction>("dummy");
            _returnData = new ReturnData();
            t.RegisterScopedFunctionAlias("returnData", _ => _returnData);
            t.BuildServiceProvider().GetService<IExpressionEngine>();

            t.AddSingleton<ExpressionGrammar>();

            _expressionGrammar = t.BuildServiceProvider().GetService<ExpressionGrammar>();
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

    internal class ReturnData : IFunction
    {
        internal ValueContainer ValueContainer;
        internal ValueContainer[] Parameters;

        public  ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            Parameters = parameters;
            return new ValueTask<ValueContainer>(ValueContainer);
        }
    }
}