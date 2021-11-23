using System.Collections.Generic;
using System.Threading.Tasks;
using ExpressionEngine;
using ExpressionEngine.Functions.Base;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

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
            services.AddScoped<VariablesFunction>();
            services.AddScoped<IFunction, VariablesFunction>(x => x.GetRequiredService<VariablesFunction>());
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
        
            Assert.AreEqual(ValueType.Boolean, output.Type());
            Assert.AreEqual(true, output.GetValue<bool>());
        }
        
        [Test]
        public async Task Test1()
        {
            const string str = "@equal(variables().account['name'], 'John Doe')";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
            Assert.AreEqual(ValueType.Boolean, output.Type());
            Assert.AreEqual(true, output.GetValue<bool>());
        }
        
        [Test]
        public async Task Test2()
        {
            const string str = "@equal(variables()['account'].name, 'John Doe')";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
             Assert.AreEqual(ValueType.Boolean, output.Type());
            Assert.AreEqual(true, output.GetValue<bool>());
        }
        
        [Test]
        public async Task Test3()
        {
            const string str = "@equal(variables().account.name, 'John Doe')";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
            Assert.AreEqual(ValueType.Boolean, output.Type());
            Assert.AreEqual(true, output.GetValue<bool>());
        }
        
        [Test]
        public async Task Test4()
        {
            const string str = "@equal(variables().account?.name, 'John Doe')";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
            Assert.AreEqual(ValueType.Boolean, output.Type());
            Assert.AreEqual(true, output.GetValue<bool>());
        }
        
        
        [Test]
        public async Task Test5()
        {
            const string str = "@variables().account";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
             
            var output = await ee.ParseToValueContainer(str);
        
            Assert.AreEqual(ValueType.Object, output.Type());
            Assert.AreEqual("John Doe", output.AsDict()["name"].GetValue<string>());
        }

        [Test]
        public async Task Test6()
        {
            const string str = "@{variables().account.name}";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
            Assert.AreEqual(ValueType.String, output.Type());
            Assert.AreEqual("John Doe", output.GetValue<string>());
        }
        
        [Test]
        public async Task Test7()
        {
            const string str = "@variables()['account']";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
            Assert.AreEqual(ValueType.Object, output.Type());
            Assert.AreEqual("John Doe", output.AsDict()["name"].GetValue<string>());
        }
        
                
        [Test]
        public async Task Test8()
        {
            const string str = "@{variables()['account']['name']}";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
            Assert.AreEqual(ValueType.String, output.Type());
            Assert.AreEqual("John Doe", output.GetValue<string>());
        }
        
        [Test]
        public async Task Test9()
        {
            const string str = "@{variables()?['account']?['name']}";
            var ee = _serviceProvider.GetRequiredService<IExpressionEngine>();
            
            var output = await ee.ParseToValueContainer(str);
        
            Assert.AreEqual(ValueType.String, output.Type());
            Assert.AreEqual("John Doe", output.GetValue<string>());
        }
    }
}