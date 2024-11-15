﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ExpressionEngine;
using ExpressionEngine.Functions.Base;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;using NUnit.Framework.Legacy;

namespace Test
{
    [TestFixture]
    public class IndexTest
    {
        private static ServiceProvider _serviceProvider;

        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection();
            services.AddExpressionEngine();
            services.RegisterScopedFunctionAlias<VariablesFunction>("variables");
            _serviceProvider = services.BuildServiceProvider();

            _serviceProvider.GetRequiredService<VariablesFunction>().DefaultValueContainer = new ValueContainer(
                new Dictionary<string, ValueContainer>
                {
                    {
                        "account", new ValueContainer(new Dictionary<string, ValueContainer>
                        {
                            {"name", new ValueContainer("John Doe")}
                        })
                    }
                });
        }

        [Test]
        public async Task Test()
        {
            const string str = "@equal(variables()['account']['name'], 'John Doe')";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
            ClassicAssert.AreEqual(ValueType.Boolean, output.Type());
            ClassicAssert.AreEqual(true, output.GetValue<bool>());
        }
        
        [Test]
        public async Task Test1()
        {
            const string str = "@equal(variables().account['name'], 'John Doe')";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
            ClassicAssert.AreEqual(ValueType.Boolean, output.Type());
            ClassicAssert.AreEqual(true, output.GetValue<bool>());
        }
        
        [Test]
        public async Task Test2()
        {
            const string str = "@equal(variables()['account'].name, 'John Doe')";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
             ClassicAssert.AreEqual(ValueType.Boolean, output.Type());
            ClassicAssert.AreEqual(true, output.GetValue<bool>());
        }
        
        [Test]
        public async Task Test3()
        {
            const string str = "@equal(variables().account.name, 'John Doe')";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
            ClassicAssert.AreEqual(ValueType.Boolean, output.Type());
            ClassicAssert.AreEqual(true, output.GetValue<bool>());
        }
        
        [Test]
        public async Task Test4()
        {
            const string str = "@equal(variables().account?.name, 'John Doe')";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
            ClassicAssert.AreEqual(ValueType.Boolean, output.Type());
            ClassicAssert.AreEqual(true, output.GetValue<bool>());
        }
        
        
        [Test]
        public async Task Test5()
        {
            const string str = "@variables().account";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
             
            var output = await ee.ParseToValueContainer(str);
        
            ClassicAssert.AreEqual(ValueType.Object, output.Type());
            ClassicAssert.AreEqual("John Doe", output.AsDict()["name"].GetValue<string>());
        }

        [Test]
        public async Task Test6()
        {
            const string str = "@{variables().account.name}";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
            ClassicAssert.AreEqual(ValueType.String, output.Type());
            ClassicAssert.AreEqual("John Doe", output.GetValue<string>());
        }
        
        [Test]
        public async Task Test7()
        {
            const string str = "@variables()['account']";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
            ClassicAssert.AreEqual(ValueType.Object, output.Type());
            ClassicAssert.AreEqual("John Doe", output.AsDict()["name"].GetValue<string>());

            var t = new ValueContainer();
        }
        
                
        [Test]
        public async Task Test8()
        {
            const string str = "@{variables()['account']['name']}";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
            ClassicAssert.AreEqual(ValueType.String, output.Type());
            ClassicAssert.AreEqual("John Doe", output.GetValue<string>());
        }
        
        [Test]
        public async Task Test9()
        {
            const string str = "@{variables()?['account']?['name']}";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
            ClassicAssert.AreEqual(ValueType.String, output.Type());
            ClassicAssert.AreEqual("John Doe", output.GetValue<string>());
        }
    }
}