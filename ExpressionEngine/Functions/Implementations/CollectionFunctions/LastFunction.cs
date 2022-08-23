using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.CollectionFunctions
{
    public class LastFunction : IFunction
    {
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            var value = parameters[0];

            return value.Type() switch
            {
                ValueType.String => new ValueTask<ValueContainer>(new ValueContainer(
                    value.GetValue<string>().ToCharArray().Last().ToString())),
                ValueType.Array => new ValueTask<ValueContainer>(new ValueContainer(
                    value.GetValue<IEnumerable<ValueContainer>>().Last())),
                _ => throw new ExpressionEngineException(
                    $"Empty expression can only operate on String or Array types, not {value.Type()}.")
            };
        }
    }
}
