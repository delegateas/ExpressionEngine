using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.CollectionFunctions
{
    public class EmptyFunction : Function
    {
        public EmptyFunction() : base("empty")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            var value = parameters[0];

            return value.Type() switch
            {
                ValueType.String => new ValueTask<ValueContainer>(
                    new ValueContainer(string.IsNullOrEmpty(value.GetValue<string>()))),
                ValueType.Array => new ValueTask<ValueContainer>(new ValueContainer(
                    !value.GetValue<IEnumerable<ValueContainer>>().Any())),
                ValueType.Object => new ValueTask<ValueContainer>(new ValueContainer(
                    value.GetValue<Dictionary<string, ValueContainer>>().Count == 0)),
                ValueType.Null => new ValueTask<ValueContainer>(new ValueContainer(true)),
                _ => throw new ExpressionEngineException(
                    $"Empty expression can only operate on String, Array or Object types, not {value.Type()}.")
            };
        }
    }
}