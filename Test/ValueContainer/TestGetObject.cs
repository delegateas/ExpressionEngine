using ExpressionEngine;
using NUnit.Framework;using NUnit.Framework.Legacy;

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

            ClassicAssert.AreEqual(typeof(string), value.GetType());
        }
    }
}