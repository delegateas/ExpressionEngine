using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class TrimFunction : Function
    {
        public TrimFunction() : base("trim")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentError(parameters.Length > 1 ? "Too many arguments" : "Too few arguments");
            }

            return new ValueTask<ValueContainer>(new ValueContainer(AuxiliaryMethods.VcIsString(parameters[0]).Trim()));
        }
    }
}