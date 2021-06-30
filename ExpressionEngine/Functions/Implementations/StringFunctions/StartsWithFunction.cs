using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class StartsWithFunction : Function
    {
        public StartsWithFunction() : base("startsWith")
        {
        }

        public override ValueContainer ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentError("Too few arguments");
            }

            return new ValueContainer(AuxiliaryMethods.VcIsString(parameters[0])
                .StartsWith(AuxiliaryMethods.VcIsString(parameters[1])));
        }
    }
}