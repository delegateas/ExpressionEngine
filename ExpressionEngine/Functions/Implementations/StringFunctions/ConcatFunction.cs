using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class ConcatFunction : Function
    {
        public ConcatFunction() : base("concat")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentError("Too few arguments");
            }

            return new ValueTask<ValueContainer>(new ValueContainer(parameters.Aggregate("", (current, value) => current + AuxiliaryMethods.VcIsString(value))));
        }
    }
}