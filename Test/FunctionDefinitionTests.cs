using System.Threading.Tasks;
using ExpressionEngine;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Test
{
    public class FunctionDefinitionTests
    {
        private IExpressionEngine _ee;

        [SetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddExpressionEngine();
            serviceCollection.AddFunctionDefinition("addAndConcat()", "concat('result of 1+1 is: ', add(1,1))");

            _ee = serviceCollection.BuildServiceProvider().GetRequiredService<IExpressionEngine>();
        }

        [TestCase]
        public async Task TestFunctionDef()
        {
            const string expectedResult = "result of 1+1 is: 2!";

            var actualResult = await _ee.Parse("@concat(addAndConcat(), '!')");

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}