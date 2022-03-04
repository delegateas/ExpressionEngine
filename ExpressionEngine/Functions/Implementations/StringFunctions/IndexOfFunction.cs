using System;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class IndexOfFunction : IFunction
    {
        public  ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentError("Too few arguments");
            }

            return new ValueTask<ValueContainer>(new ValueContainer(AuxiliaryMethods.VcIsString(parameters[0])
                .IndexOf(AuxiliaryMethods.VcIsString(parameters[1]), StringComparison.OrdinalIgnoreCase)));
        }
    }
}
