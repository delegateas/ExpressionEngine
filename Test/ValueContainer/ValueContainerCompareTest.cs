using System.Collections.Generic;
using ExpressionEngine;
using NUnit.Framework;using NUnit.Framework.Legacy;

namespace Test
{
    [TestFixture]
    public class ValueContainerCompareTest
    {
        [Test]
        public void TestPositive()
        {
            ClassicAssert.AreEqual(true, new ValueContainer().Equals(new ValueContainer()));
            ClassicAssert.AreEqual(true, new ValueContainer(1).Equals(new ValueContainer(1)));
            ClassicAssert.AreEqual(true, new ValueContainer("2").Equals(new ValueContainer("2")));
            ClassicAssert.AreEqual(true, new ValueContainer(true).Equals(new ValueContainer(true)));
            ClassicAssert.AreEqual(true, new ValueContainer(2.1).Equals(new ValueContainer(2.1)));
            ClassicAssert.AreEqual(true, new ValueContainer(2.1f).Equals(new ValueContainer(2.1f)));
            ClassicAssert.AreEqual(true,
                new ValueContainer(new ValueContainer[] { }).Equals(new ValueContainer(new ValueContainer[] { })));
            ClassicAssert.AreEqual(true,
                new ValueContainer(new[] {new ValueContainer("1"), new ValueContainer("2")}).Equals(
                    new ValueContainer(new[] {new ValueContainer("1"), new ValueContainer("2")})));
            ClassicAssert.AreEqual(true,
                new ValueContainer(new Dictionary<string, ValueContainer>()).Equals(
                    new ValueContainer(new Dictionary<string, ValueContainer>())));
            ClassicAssert.AreEqual(true,
                new ValueContainer(new Dictionary<string, ValueContainer>()
                    {{"key1", new ValueContainer("value1")}, {"key2", new ValueContainer(true)}}).Equals(
                    new ValueContainer(new Dictionary<string, ValueContainer>()
                        {{"key2", new ValueContainer(true)}, {"key1", new ValueContainer("value1")}})));
        }

        [Test]
        public void TestNegative()
        {
            ClassicAssert.AreEqual(false, new ValueContainer().Equals(new ValueContainer("")));
            ClassicAssert.AreEqual(false, new ValueContainer("321").Equals(new ValueContainer("123")));
            ClassicAssert.AreEqual(false, new ValueContainer(1).Equals(new ValueContainer(2)));
            ClassicAssert.AreEqual(false, new ValueContainer(1.2).Equals(new ValueContainer(2.1)));
            ClassicAssert.AreEqual(false, new ValueContainer(1.2f).Equals(new ValueContainer(2.1f)));
            ClassicAssert.AreEqual(false, new ValueContainer(false).Equals(new ValueContainer(true)));
            ClassicAssert.AreEqual(false,
                new ValueContainer(new[] {new ValueContainer("")}).Equals(new ValueContainer(new ValueContainer[]
                    { })));
            ClassicAssert.AreEqual(false,
                new ValueContainer(new Dictionary<string, ValueContainer>() {{"key1", new ValueContainer("true")}})
                    .Equals(new ValueContainer(new Dictionary<string, ValueContainer>()
                        {{"key1", new ValueContainer(true)}})));
        }

        [Test]
        public void TestDifferentTypes()
        {
            ClassicAssert.AreEqual(false, new ValueContainer().Equals(new ValueContainer("")));
            ClassicAssert.AreEqual(false, new ValueContainer("").Equals(new ValueContainer(2)));
            ClassicAssert.AreEqual(false, new ValueContainer("").Equals(new ValueContainer(true)));
            ClassicAssert.AreEqual(false, new ValueContainer("").Equals(new ValueContainer(new ValueContainer[] {})));
            ClassicAssert.AreEqual(false, new ValueContainer("").Equals(new ValueContainer(new Dictionary<string, ValueContainer>())));
            ClassicAssert.AreEqual(false, new ValueContainer(2).Equals(new ValueContainer(new ValueContainer[] {})));
           
            ClassicAssert.IsTrue(new ValueContainer(2).Equals(new ValueContainer(2f)));
            ClassicAssert.IsTrue(new ValueContainer(2).Equals(new ValueContainer(2d)));
            ClassicAssert.IsTrue(new ValueContainer(2).Equals(new ValueContainer(2)));
            ClassicAssert.IsTrue(new ValueContainer(2f).Equals(new ValueContainer(2)));
            ClassicAssert.IsTrue(new ValueContainer(2d).Equals(new ValueContainer(2)));
            ClassicAssert.IsTrue(new ValueContainer(2d).Equals(new ValueContainer(2f)));
            ClassicAssert.IsTrue(new ValueContainer(2f).Equals(new ValueContainer(2d)));
            ClassicAssert.IsFalse(new ValueContainer(2d).Equals(new ValueContainer(3)));
        }
    }
}