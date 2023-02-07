using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.ConversionFunctions
{
    [FunctionRegistration("dataUri")]
    public class DataUriFunction : IFunction
    {
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length == 0)
            {
                throw InvalidTemplateException.BuildInvalidLanguageFunction("SomeActon", "dataUri");
            }

            return parameters[0].Type() switch
            {
                ValueType.String =>
                    new ValueTask<ValueContainer>(new ValueContainer(
                        $"data:text/plain;charset=utf-8;base64,{System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(parameters[0].GetValue<string>()))}")),
                _ => throw new ExpressionEngineException(
                    $"Array function can only operate on strings, not {parameters[0].Type()}.")
            };
        }
    }
}
