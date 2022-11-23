using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.ConversionFunctions
{
    [FunctionRegistration("dataUriToBinary")]
    public class DataUriToBinaryFunction : IFunction
    {
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length == 0)
            {
                throw InvalidTemplateException.BuildInvalidLanguageFunction("SomeActon", "dataUriToBinary");
            }

            return parameters[0].Type() switch
            {
                ValueType.String =>
                    new ValueTask<ValueContainer>(new ValueContainer(Encoding.UTF8.GetBytes(parameters[0].GetValue<string>())
                        .Aggregate("", (s, b) => s + Convert.ToString(b, 2).PadLeft(8, '0')))),
                _ => throw new ExpressionEngineException(
                    $"Array function can only operate on strings, not {parameters[0].Type()}.")
            };
        }
    }
}
