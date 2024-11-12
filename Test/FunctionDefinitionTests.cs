using System;
using System.Threading.Tasks;
using ExpressionEngine;
using ExpressionEngine.Functions.CustomException;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;using NUnit.Framework.Legacy;
using NUnit.Framework.Legacy;

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

            ClassicAssert.AreEqual(expectedResult, actualResult);
        }

        [TestCase]
        public void TestFunctionException()
        {
            const string expectedMessage = "fromFunctionName cannot end in ()";
            
            var exception = ClassicAssert.Throws<ArgumentError>(() =>
            {
                _serviceCollection.AddFunctionDefinition("addAndConcat()", "concat('result of 1+1 is: ', add(1,1))");
            });
            
            ClassicAssert.NotNull(exception);
            ClassicAssert.AreEqual(expectedMessage,exception.Message);
        }
    }
}