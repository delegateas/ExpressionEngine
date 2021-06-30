﻿using System.Collections.Generic;
using ExpressionEngine;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class ValueContainerCompareTest
    {
        [Test]
        public void TestPositive()
        {
            Assert.AreEqual(true, new ValueContainer().Equals(new ValueContainer()));
            Assert.AreEqual(true, new ValueContainer(1).Equals(new ValueContainer(1)));
            Assert.AreEqual(true, new ValueContainer("2").Equals(new ValueContainer("2")));
            Assert.AreEqual(true, new ValueContainer(true).Equals(new ValueContainer(true)));
            Assert.AreEqual(true, new ValueContainer(2.1).Equals(new ValueContainer(2.1)));
            Assert.AreEqual(true, new ValueContainer(2.1f).Equals(new ValueContainer(2.1f)));
            Assert.AreEqual(true,
                new ValueContainer(new ValueContainer[] { }).Equals(new ValueContainer(new ValueContainer[] { })));
            Assert.AreEqual(true,
                new ValueContainer(new[] {new ValueContainer("1"), new ValueContainer("2")}).Equals(
                    new ValueContainer(new[] {new ValueContainer("1"), new ValueContainer("2")})));
            Assert.AreEqual(true,
                new ValueContainer(new Dictionary<string, ValueContainer>()).Equals(
                    new ValueContainer(new Dictionary<string, ValueContainer>())));
            Assert.AreEqual(true,
                new ValueContainer(new Dictionary<string, ValueContainer>()
                    {{"key1", new ValueContainer("value1")}, {"key2", new ValueContainer(true)}}).Equals(
                    new ValueContainer(new Dictionary<string, ValueContainer>()
                        {{"key2", new ValueContainer(true)}, {"key1", new ValueContainer("value1")}})));
        }

        [Test]
        public void TestNegative()
        {
            Assert.AreEqual(false, new ValueContainer().Equals(new ValueContainer("")));
            Assert.AreEqual(false, new ValueContainer("321").Equals(new ValueContainer("123")));
            Assert.AreEqual(false, new ValueContainer(1).Equals(new ValueContainer(2)));
            Assert.AreEqual(false, new ValueContainer(1.2).Equals(new ValueContainer(2.1)));
            Assert.AreEqual(false, new ValueContainer(1.2f).Equals(new ValueContainer(2.1f)));
            Assert.AreEqual(false, new ValueContainer(false).Equals(new ValueContainer(true)));
            Assert.AreEqual(false,
                new ValueContainer(new[] {new ValueContainer("")}).Equals(new ValueContainer(new ValueContainer[]
                    { })));
            Assert.AreEqual(false,
                new ValueContainer(new Dictionary<string, ValueContainer>() {{"key1", new ValueContainer("true")}})
                    .Equals(new ValueContainer(new Dictionary<string, ValueContainer>()
                        {{"key1", new ValueContainer(true)}})));
        }

        [Test]
        public void TestDifferentTypes()
        {
            Assert.AreEqual(false, new ValueContainer().Equals(new ValueContainer("")));
            Assert.AreEqual(false, new ValueContainer("").Equals(new ValueContainer(2)));
            Assert.AreEqual(false, new ValueContainer("").Equals(new ValueContainer(true)));
            Assert.AreEqual(false, new ValueContainer("").Equals(new ValueContainer(new ValueContainer[] {})));
            Assert.AreEqual(false, new ValueContainer("").Equals(new ValueContainer(new Dictionary<string, ValueContainer>())));
            Assert.AreEqual(false, new ValueContainer(2).Equals(new ValueContainer(new ValueContainer[] {})));
           
            Assert.IsTrue(new ValueContainer(2).Equals(new ValueContainer(2f)));
            Assert.IsTrue(new ValueContainer(2).Equals(new ValueContainer(2d)));
            Assert.IsTrue(new ValueContainer(2).Equals(new ValueContainer(2)));
            Assert.IsTrue(new ValueContainer(2f).Equals(new ValueContainer(2)));
            Assert.IsTrue(new ValueContainer(2d).Equals(new ValueContainer(2)));
            Assert.IsTrue(new ValueContainer(2d).Equals(new ValueContainer(2f)));
            Assert.IsTrue(new ValueContainer(2f).Equals(new ValueContainer(2d)));
            Assert.IsFalse(new ValueContainer(2d).Equals(new ValueContainer(3)));
        }
    }
}