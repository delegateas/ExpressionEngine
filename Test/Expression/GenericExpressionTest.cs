using ExpressionEngine;
using ExpressionEngine.Functions.Base;
using NUnit.Framework;

namespace Test.Expression
{
    [TestFixture]
    public class GenericExpressionTest
    {
        [Test, TestCaseSource(typeof(StringFunctionTests), nameof(StringFunctionTests.StringFunctionTestInput))]
        [TestCaseSource(typeof(CollectionFunctionTests), nameof(CollectionFunctionTests.CollectionFunctionTestInput))]
        [TestCaseSource(typeof(LogicalFunctionTest), nameof(LogicalFunctionTest.LogicalFunctionTestInput))]
        [TestCaseSource(typeof(ConversionFunctionTest), nameof(ConversionFunctionTest.ConversionFunctionTestInput))]
        [TestCaseSource(typeof(MathFunctionTests), nameof(MathFunctionTests.MathFunctionTestInput))]
        public void TestFunction(Function func, string name,
            ValueContainer[] parameters, ValueContainer expected)
        {
            var result = func.ExecuteFunction(parameters);

            Assert.AreEqual(name, func.FunctionName);
            Assert.AreEqual(expected, result);
        }
    }
}