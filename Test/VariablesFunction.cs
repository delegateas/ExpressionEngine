using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine;
using ExpressionEngine.Functions.Base;

namespace Test
{
    public class VariablesFunction : IFunction
    {
        public ValueContainer DefaultValueContainer { get; set; }
        private readonly ValueContainer _indexedValueContainer = new ValueContainer(new Dictionary<string, ValueContainer>());

        public async ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
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
