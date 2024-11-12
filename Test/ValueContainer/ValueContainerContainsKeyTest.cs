using System.Collections.Generic;
using ExpressionEngine;
using NUnit.Framework;using NUnit.Framework.Legacy;

namespace Test
{
    [TestFixture]
    public class ValueContainerContainsKeyTest
    {

        [Test]
        public void TestContainsKey()
        {
            var vc = new ValueContainer(new Dictionary<string, ValueContainer>
            {
                {"layer0/layer1/layer2", new ValueContainer("String value")}
            });
            
            ClassicAssert.IsTrue(vc.ContainsKey("layer0/layer1/layer2"));
            ClassicAssert.IsTrue(vc.ContainsKey("layer0/layer1"));
            ClassicAssert.IsFalse(vc.ContainsKey("layer1/layer2"));
        }
    }
}