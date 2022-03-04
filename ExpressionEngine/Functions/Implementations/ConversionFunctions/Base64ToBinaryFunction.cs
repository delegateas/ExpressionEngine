using System;
using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.ConversionFunctions
{
    public class Base64ToBinaryFunction : IFunction
    {
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            return parameters[0].Type() switch
            {
                ValueType.String =>
                    new ValueTask<ValueContainer>(new ValueContainer(Convert.FromBase64String(parameters[0].GetValue<string>())
                        .Aggregate("", (s, b) => s + Convert.ToString(b, 2).PadLeft(8, '0')))),
                _ => throw new ExpressionEngineException(
                    $"Array function can only operate on strings, not {parameters[0].Type()}.")
            };
        }
    }
}
