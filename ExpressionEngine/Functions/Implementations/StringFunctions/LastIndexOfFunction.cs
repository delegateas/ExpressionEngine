using System;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class LastIndexOfFunction : Function
    {
        public LastIndexOfFunction() : base("lastIndexOf")
        {
        }

        public override ValueContainer ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentError("Too few arguments");
            }

            if (string.IsNullOrEmpty(AuxiliaryMethods.VcIsString(parameters[1])))
            {
                /* LastIndexOf does not return the correct value!
                 * "".LastIndexOf("") should return -1, since "" is empty
                 * and "abc".LastIndexOf("") returns `2` and `3` for dotnetcore3.1 and net5.0 respectively,
                 * which does not make sense.
                 * Therefore, if the search string is empty, we return the length of the string being searched,
                 * because it corresponds to the behavior of PowerAutomate.
                 *
                 * -1 because the returned index is zero-based
                 */
                if (string.IsNullOrEmpty(AuxiliaryMethods.VcIsString(parameters[0])))
                {
                    return new ValueContainer(0);
                }
                return new ValueContainer(AuxiliaryMethods.VcIsString(parameters[0]).Length-1);
            }
    
            return new ValueContainer(AuxiliaryMethods.VcIsString(parameters[0])
                .LastIndexOf(AuxiliaryMethods.VcIsString(parameters[1]), StringComparison.OrdinalIgnoreCase));
        }
    }
}