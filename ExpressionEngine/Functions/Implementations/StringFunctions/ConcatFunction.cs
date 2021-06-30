using System.Linq;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class ConcatFunction : Function
    {
        public ConcatFunction() : base("concat")
        {
        }

        public override ValueContainer ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentError("Too few arguments");
            }

            return new ValueContainer(parameters.Aggregate("", (current, value) => current + AuxiliaryMethods.VcIsString(value)));
        }
    }
}