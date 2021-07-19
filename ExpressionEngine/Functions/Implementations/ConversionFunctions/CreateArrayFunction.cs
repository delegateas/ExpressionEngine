using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.ConversionFunctions
{
    public class CreateArrayFunction : Function
    {
        public CreateArrayFunction() : base("createArray")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length == 0)
            {
                throw InvalidTemplateException.BuildInvalidLanguageFunction("SomeActon", "createArray");
            }

            return new ValueTask<ValueContainer>(new ValueContainer(parameters.ToList()));
        }
    }
}