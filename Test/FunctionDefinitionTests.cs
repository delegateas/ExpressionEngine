using System;
using System.Threading.Tasks;
using ExpressionEngine;
using ExpressionEngine.Functions.CustomException;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Test
{
    public class FunctionDefinitionTests
    {
        private ServiceCollection _serviceCollection;

        [SetUp]
        public void Setup()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddExpressionEngine();
            _serviceCollection.AddFunctionDefinition("addAndConcat", "concat('result of 1+1 is: ', add(1,1))");
        }

        [TestCase]
        public async Task TestFunctionDef()
        {
            var ee = _serviceCollection.BuildServiceProvider().GetRequiredService<IExpressionEngine>();
            const string expectedResult = "result of 1+1 is: 2!";

            var actualResult = await ee.Parse("@concat(addAndConcat(), '!')");

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase]
        public void TestFunctionException()
        {
            const string expectedMessage = "fromFunctionName cannot end in ()";
            
            var exception = Assert.Throws<ArgumentError>(() =>
            {
                _serviceCollection.AddFunctionDefinition("addAndConcat()", "concat('result of 1+1 is: ', add(1,1))");
            });
            
            Assert.NotNull(exception);
            Assert.AreEqual(expectedMessage,exception.Message);
        }
    }
}