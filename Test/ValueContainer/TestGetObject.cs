using ExpressionEngine;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class TestGetObject
    {
        [TestCase]
        public void TestImplicitConversionThingy()
        {
            var valueContainer = new ValueContainer("Hello CLR Type!");

            var value = valueContainer.GetValue<object>();

            Assert.AreEqual(typeof(string), value.GetType());
        }
    }
}