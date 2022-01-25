using System;
using System.Threading.Tasks;
using ExpressionEngine;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ValueType = ExpressionEngine.ValueType;


namespace Test
{
    public class AsynctestFunction : Function
    {
        public AsynctestFunction() : base("asynctest")
        {

        }

        public override async ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            await Task.Delay((int) (new Random().NextDouble()* 10000));
            return new ValueContainer("Hello World");
        }
    }
    [TestFixture]
    public class ParserTest
    {
        private ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddExpressionEngine();
            services.AddTransient<IFunction, AsynctestFunction>();
            return services.BuildServiceProvider();
        }

        #region ErrorHandlingTest

        [TestCaseSource(nameof(_simpleCasesExceptions))]
        public void TestSimpleInputExpectFail(TestInput testInput)
        {
            var sp = BuildServiceProvider();
            var engine = sp.GetRequiredService<IExpressionEngine>();

            var exception = Assert.ThrowsAsync(Is.InstanceOf(testInput.ExceptionType),
                async () => { await engine.Parse(testInput.Input); });

            Assert.AreEqual(testInput.ExpectedOutput.GetValue<string>(), exception.Message);
        }

        private static TestInput[] _simpleCasesExceptions =
        {
            new TestInput(
                "@nonExistingFunction(\'input\')",
                new ValueContainer(
                    "Function with name: nonExistingFunction, does not exist.\nAdd the function to the expression engine to test this expression.")
            )
            {
                ExceptionType = typeof(FunctionNotKnown)
            },
            new TestInput(
                "@nonExistingFunction()",
                new ValueContainer(
                    "Function with name: nonExistingFunction, does not exist.\nAdd the function to the expression engine to test this expression.")
            )
            {
                ExceptionType = typeof(FunctionNotKnown)
            }
        };

        #endregion

        #region SimpleCases

        [TestCaseSource(nameof(_simpleCases))]
        public async Task TestSimpleInput(TestInput testInput)
        {
            var sp = BuildServiceProvider();
            var engine = sp.GetRequiredService<IExpressionEngine>();

            var result = await engine.ParseToValueContainer(testInput.Input);

            Assert.AreEqual(testInput.ExpectedOutput, result);
        }

        private static TestInput[] _simpleCases =
        {
            new TestInput("Just a simple string without an exceptions",
                new ValueContainer("Just a simple string without an exceptions")),
            new TestInput("tst@delegate.dk", new ValueContainer("tst@delegate.dk")),
            new TestInput(
                "@{toLower(\'user@DOMAIN.dk\')} - Send me an email or call on 8888 8888!", new ValueContainer(
                    "user@domain.dk - Send me an email or call on 8888 8888!")
            ),
            new TestInput("@concat(\'Hi you,\',\' Alice B.\')", new ValueContainer("Hi you, Alice B.")),
            new TestInput(
                "@concat(\'Hej med dig\', \' Bob A\', \' hvordan \', \'går det?\')", new ValueContainer(
                    "Hej med dig Bob A hvordan går det?")
            ),
            new TestInput("@concat(\'Hi you,\', toLower(\' ALICE B\'))", new ValueContainer("Hi you, alice b")),
            new TestInput("@trim(\' What is up \')", new ValueContainer("What is up")),
            new TestInput("@toLower(\'It is John''s car.\')", new ValueContainer("it is john's car.")),
            new TestInput("Hej med dig @{toUpper(\'charlie\')}", new ValueContainer("Hej med dig CHARLIE")),
            new TestInput("@{toLower(\'Hej med dig \')}@{trim(\' Mads \')}", new ValueContainer("hej med dig Mads")),
            new TestInput("@@1", new ValueContainer("@1")),
            new TestInput("aes", new ValueContainer("aes")),
            new TestInput("@greater(11, 10)", new ValueContainer(true)),
            new TestInput("@greater(10.1, 10.0)", new ValueContainer(true)),
            new TestInput("@greater(10.1, 10)", new ValueContainer(true)),
            new TestInput("@add(10,0.5)", new ValueContainer(10.5)),
            

        };

        #endregion

        [TestCaseSource(nameof(_async))]
        public async Task TestAsync(TestInput testInput)
        {
            var sp = BuildServiceProvider();
            var engine = sp.GetRequiredService<IExpressionEngine>();

            var result = await engine.ParseToValueContainer(testInput.Input);

            Assert.AreEqual(testInput.ExpectedOutput, result);
        }

        private static TestInput[] _async =
      {
           
            new TestInput("@{asynctest()} - @{asynctest()} - @{asynctest()}", new ValueContainer("Hello World - Hello World - Hello World")),

        };

     

        public class TestInput
        {
            public TestInput(string input, ValueContainer expectedOutput)
            {
                Input = input;
                ExpectedOutput = expectedOutput;
            }
            public ValueContainer[] ValueContainers { get; set; }
            public string Input { get; set; }
            public ValueContainer ExpectedOutput { get; set; }
            public ValueType ExpectedOutputType { get; set; }

            public Type ExceptionType { get; set; }
        }
    }
}