using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.CollectionFunctions
{
    [FunctionRegistration("take")]
    public class TakeFunction : IFunction
    {
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            var value = parameters[0];
            var count = parameters[1].GetValue<int>();

            return value.Type() switch
            {
                ValueType.Array => new ValueTask<ValueContainer>(new ValueContainer(
                    value.GetValue<IEnumerable<ValueContainer>>().Take(count))),
                _ => throw new ExpressionEngineException(
                    $"Empty expression can only operate on String or Array types, not {value.Type()}.")
            };
        }
    }
}
