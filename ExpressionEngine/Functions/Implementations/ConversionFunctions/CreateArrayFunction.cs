using System.Linq;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.ConversionFunctions
{
    public class CreateArrayFunction : Function
    {
        public CreateArrayFunction() : base("createArray")
        {
        }

        public override ValueContainer ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length == 0)
            {
                throw InvalidTemplateException.BuildInvalidLanguageFunction("SomeActon", "createArray");
            }

            return new ValueContainer(parameters.ToList());
        }
    }
}