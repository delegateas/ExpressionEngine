using System;
using ExpressionEngine;
using ExpressionEngine.Functions.CustomException;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;


namespace Test
{
    [TestFixture]
    public class ParserTest
    {
        private ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddExpressionEngine();

            return services.BuildServiceProvider();
        }

        #region ErrorHandlingTest

        [TestCaseSource(nameof(_simpleCasesExceptions))]
        public void TestSimpleInputExpectFail(TestInput testInput)
        {
            var sp = BuildServiceProvider();
            var engine = sp.GetRequiredService<IExpressionEngine>();

            var exception = Assert.Throws(Is.InstanceOf(testInput.ExceptionType),
                () => { engine.Parse(testInput.Input); });

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
        public void TestSimpleInput(TestInput testInput)
        {
            var sp = BuildServiceProvider();
            var engine = sp.GetRequiredService<IExpressionEngine>();

            var result = engine.ParseToValueContainer(testInput.Input);

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
            new TestInput("@aes", new ValueContainer("aes")),
            new TestInput("@empty(trim(' ')?.name1)", new ValueContainer(true)),
            new TestInput("@not(empty(trim(' ')?.name1))", new ValueContainer(false))
        };

        #endregion
        
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
            public ValueContainer.ValueType ExpectedOutputType { get; set; }

            public Type ExceptionType { get; set; }
        }
    }
}