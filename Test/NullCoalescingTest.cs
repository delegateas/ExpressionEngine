using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine;
using ExpressionEngine.Functions.Base;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ValueType = ExpressionEngine.ValueType;

namespace Test
{
    [TestFixture]
    public class NullCoalescingTest
    {
        private static ServiceProvider _serviceProvider;

        private IServiceProvider BuildServiceProvider()
        {
            if (_serviceProvider == null)
            {
                var services = new ServiceCollection();
                services.AddExpressionEngine();
                services.AddScoped<VariablesFunction>();
                services.AddScoped<IFunction, VariablesFunction>(x => x.GetRequiredService<VariablesFunction>());
                _serviceProvider = services.BuildServiceProvider();
            }

            return _serviceProvider.CreateScope().ServiceProvider;
        }

        #region TriggerOutputs

        [TestCaseSource(nameof(_triggerOutputTest))]
        public async Task TestTriggerOutputs(TestInput testInput)
        {
            var sp = BuildServiceProvider();
            var engine = sp.GetRequiredService<IExpressionEngine>();
            var variablesFunction = sp.GetRequiredService<VariablesFunction>();
            variablesFunction.DefaultValueContainer = testInput.ValueContainers.First();

            var result = await engine.ParseToValueContainer(testInput.Input);

            Assert.AreEqual(testInput.ExpectedOutput, result);
        }

        private static object[] _triggerOutputTest =
        {
            new TestInput("@variables()['body/entrytype']", new ValueContainer("OCR payment"))
            {
                ValueContainers = new[]
                {
                    new ValueContainer(new Dictionary<string, ValueContainer>
                    {
                        {"body/entrytype", new ValueContainer("OCR payment")}
                    })
                }
            },
            new TestInput("@variables()?['body/entrytype']", new ValueContainer())
            {
                ValueContainers = new[]
                {
                    new ValueContainer(new Dictionary<string, ValueContainer>
                    {
                        {"", new ValueContainer("")}
                    })
                }
            },
            new TestInput("@variables()['body/contactid']",
                new ValueContainer("00112233-4455-6677-8899-aabbccddeeff"))
            {
                ValueContainers = new[]
                {
                    new ValueContainer(new Dictionary<string, ValueContainer>
                    {
                        {
                            "body",
                            new ValueContainer(new Dictionary<string, ValueContainer>
                                {{"contactid", new ValueContainer("00112233-4455-6677-8899-aabbccddeeff")}})
                        }
                    })
                }
            },
            new TestInput("@variables()['body']['contactid']",
                new ValueContainer("00112233-4455-6677-8899-aabbccddeeff"))
            {
                ValueContainers = new[]
                {
                    new ValueContainer(new Dictionary<string, ValueContainer>
                    {
                        {
                            "body",
                            new ValueContainer(new Dictionary<string, ValueContainer>
                                {{"contactid", new ValueContainer("00112233-4455-6677-8899-aabbccddeeff")}})
                        }
                    })
                }
            },
            new TestInput("@variables()['body']?['contactid']",
                new ValueContainer())
            {
                ValueContainers = new[]
                {
                    new ValueContainer(new Dictionary<string, ValueContainer>
                    {
                        {
                            "body",
                            new ValueContainer(new Dictionary<string, ValueContainer>
                                {{"accountid", new ValueContainer("00112233-4455-6677-8899-aabbccddeeff")}})
                        }
                    })
                }
            }
        };

        #endregion

        #region ErrorHandlingTest

        [TestCaseSource(nameof(_simpleCasesExceptions))]
        public void TestSimpleInputExpectFail(TestInput testInput)
        {
            var sp = BuildServiceProvider();
            var engine = sp.GetRequiredService<IExpressionEngine>();
            var variablesFunction = sp.GetRequiredService<VariablesFunction>();
            variablesFunction.DefaultValueContainer = testInput.ValueContainers.First();

            var exception = Assert.ThrowsAsync(Is.InstanceOf(testInput.ExceptionType),
                async () => { await engine.Parse(testInput.Input); });

            Assert.AreEqual(testInput.ExpectedOutput.GetValue<string>(), exception.Message);
        }

        private static TestInput[] _simpleCasesExceptions =
        {
            new TestInput("@variables()?['body']['contactid']",
                new ValueContainer(
                    "InvalidTemplate. Unable to process template language expressions in action '' inputs at line 'x' " +
                    "and column 'y': 'The template language expression 'variables()['body']['contactid']' cannot be " +
                    "evaluated because because property 'contactid' doesn't exist, available properties are 'accountid'." +
                    "Please see https://aka.ms/logicexpressions for usage details.'."))
            {
                ValueContainers = new[]
                {
                    new ValueContainer(new Dictionary<string, ValueContainer>
                    {
                        {
                            "body",
                            new ValueContainer(new Dictionary<string, ValueContainer>
                                {{"accountid", new ValueContainer("00112233-4455-6677-8899-aabbccddeeff")}})
                        }
                    })
                },
                ExceptionType = typeof(Exception)
            }
        };

        #endregion

        #region TestStorage

        [TestCaseSource(nameof(_testStorage))]
        public async Task TestInternalsFlowStorage(TestInput input)
        {
            var sp = BuildServiceProvider();
            var engine = sp.GetRequiredService<IExpressionEngine>();
            var state = sp.GetRequiredService<VariablesFunction>();

            AddValuesToState(input.VariableKey, input.ValueContainers, input.StorageOption, state);

            var result = await engine.ParseToValueContainer(input.Input);

            Assert.AreEqual(input.ExpectedOutput, result);
        }


        private static readonly ValueContainer[] BigValue =
        {
            new ValueContainer(new Dictionary<string, ValueContainer>
            {
                {
                    "firstChildValue", new ValueContainer(new Dictionary<string, ValueContainer>
                    {
                        {"secondChildValue", new ValueContainer("The String Value")}
                    })
                }
            }),
            new ValueContainer("Poul")
        };

        private static TestInput[] _testStorage =
        {
            new TestInput(
                "@variables(\'array\')[0]",
                new ValueContainer("Thyge"))
            {
                VariableKey = "array",
                ValueContainers = new[] {new ValueContainer("Thyge"), new ValueContainer("Poul")},
                StorageOption = StorageOption.Variables
            },
            new TestInput(
                "@toLower(variables(\'name\'))",
                new ValueContainer("alice bob"))
            {
                VariableKey = "name",
                ValueContainers = new[] {new ValueContainer("Alice Bob")},
                StorageOption = StorageOption.Variables
            },
            new TestInput(
                "@variables('Compose_name')",
                new ValueContainer("Alice Bob"))
            {
                VariableKey = "Compose_name",
                ValueContainers = new[] {new ValueContainer("Alice Bob")},
                StorageOption = StorageOption.Outputs
            },
            new TestInput
            (
                "@variables(\'dictionary\')?[0]?[\'firstChildValue\'][\'secondChildValue\']",
                new ValueContainer("The String Value"))
            {
                VariableKey = "dictionary",
                ValueContainers = BigValue,
                StorageOption = StorageOption.Variables
            }
        };

        [TestCaseSource(nameof(_testStorageValueContainer))]
        public async Task TestInternalsFlowStorageValueContainer(TestInput input)
        {
            var sp = BuildServiceProvider();
            var engine = sp.GetRequiredService<IExpressionEngine>();
            var state = sp.GetRequiredService<VariablesFunction>();

            AddValuesToState(input.VariableKey, input.ValueContainers, input.StorageOption, state);

            var result = await engine.ParseToValueContainer(input.Input);

            Assert.AreEqual(input.ExpectedOutputType, result.Type());
        }

        private static TestInput[] _testStorageValueContainer =
        {
            new TestInput(
                "@variables(\'array\')",
                ValueType.Array)
            {
                VariableKey = "array",
                ValueContainers = new[] {new ValueContainer("Thyge"), new ValueContainer("Poul")},
                StorageOption = StorageOption.Variables
            },
            new TestInput(
                "@toLower(variables(\'name\'))",
                ValueType.String)
            {
                VariableKey = "name",
                ValueContainers = new[] {new ValueContainer("Alice Bob")},
                StorageOption = StorageOption.Variables
            },
            new TestInput(
                "@variables('Compose_name')",
                ValueType.String)
            {
                VariableKey = "Compose_name",
                ValueContainers = new[] {new ValueContainer("Alice Bob")},
                StorageOption = StorageOption.Outputs
            },
            new TestInput
            (
                "@variables(\'dictionary\')?[0]?[\'firstChildValue\'][\'secondChildValue\']",
                ValueType.String)
            {
                VariableKey = "dictionary",
                ValueContainers = BigValue,
                StorageOption = StorageOption.Variables
            },
            new TestInput
            (
                "@greater(variables('Variable'), 10)",
                ValueType.Boolean)
            {
                VariableKey = "Variable",
                ValueContainers = new[] {new ValueContainer(11)},
                StorageOption = StorageOption.Variables
            }
        };

        public class TestInput
        {
            public TestInput(string input, ValueContainer expectedOutput)
            {
                Input = input;
                ExpectedOutput = expectedOutput;
            }

            public TestInput(string input, ValueType expectedOutputType)
            {
                Input = input;
                ExpectedOutputType = expectedOutputType;
            }

            public string VariableKey { get; set; }
            public ValueContainer[] ValueContainers { get; set; }
            public string Input { get; set; }
            public ValueContainer ExpectedOutput { get; set; }
            public ValueType ExpectedOutputType { get; set; }
            public StorageOption StorageOption { get; set; }

            public Type ExceptionType { get; set; }
        }

        public enum StorageOption
        {
            Outputs,
            Variables
        }

        #endregion

        private static void AddValuesToState(string key, IReadOnlyList<ValueContainer> values, StorageOption storageOption,
            VariablesFunction state)
        {
            if (values == null)
            {
                return;
            }

            switch (storageOption)
            {
                case StorageOption.Outputs:
                    if (values.Count == 1)
                        state.AddValueContainer(key, values[0]);
                    else
                        state.AddValueContainer(key, values);
                    break;
                case StorageOption.Variables:
                    if (values.Count == 1)
                        state.AddValueContainer(key, values[0]);
                    else
                        state.AddValueContainer(key, values);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(storageOption), storageOption, null);
            }
        }
    }

    public class VariablesFunction : Function
    {
        public ValueContainer DefaultValueContainer { get; set; }
        private readonly ValueContainer _indexedValueContainer = new ValueContainer(new Dictionary<string, ValueContainer>());


        public VariablesFunction() : base("variables")
        {
        }

        public override async ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            return parameters?.Length switch
            {
                null => DefaultValueContainer,
                0 => DefaultValueContainer,
                1 => _indexedValueContainer[parameters.First().GetValue<string>()],
                _ => new ValueContainer()
            };
        }

        public void AddValueContainer(string key, ValueContainer valueContainer)
        {
            _indexedValueContainer[key] = valueContainer;
        }

        public void AddValueContainer(string key, IEnumerable<ValueContainer> valueContainer)
        {
            _indexedValueContainer[key] = new ValueContainer(valueContainer);
        }
    }
}