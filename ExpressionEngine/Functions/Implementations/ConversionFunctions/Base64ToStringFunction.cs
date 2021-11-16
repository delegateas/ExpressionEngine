using System;
using System.Text;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.ConversionFunctions
{
    public class Base64ToStringFunction : Function
    {
        public Base64ToStringFunction() : base("base64ToString")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            return parameters[0].Type() switch
            {
                ValueType.String =>
                    new ValueTask<ValueContainer>(new ValueContainer(Encoding.UTF8.GetString(Convert.FromBase64String(parameters[0].GetValue<string>())))),
                _ => throw new ExpressionEngineException(
                    $"Array function can only operate on strings, not {parameters[0].Type()}.")
            };
        }
    }
}