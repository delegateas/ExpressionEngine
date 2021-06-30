using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class EndsWithFunction : Function
    {
        public EndsWithFunction() : base("endsWith")
        {
        }

        public override ValueContainer ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentError("Too few arguments");
            }

            return new ValueContainer(AuxiliaryMethods.VcIsString(parameters[0])
                .EndsWith(AuxiliaryMethods.VcIsString(parameters[1])));
        }
    }
}