using System;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class IndexOfFunction : Function
    {
        public IndexOfFunction() : base("indexOf")
        {
        }

        public override ValueContainer ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentError("Too few arguments");
            }

            return new ValueContainer(AuxiliaryMethods.VcIsString(parameters[0])
                .IndexOf(AuxiliaryMethods.VcIsString(parameters[1]), StringComparison.OrdinalIgnoreCase));
        }
    }
}