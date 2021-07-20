using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class SubstringFunction : Function
    {
        public SubstringFunction() : base("substring")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2 || parameters.Length > 3)
            {
                throw new ArgumentError(parameters.Length > 3 ? "Too many arguments" : "Too few arguments");
            }

            var str = parameters[0].GetValue<string>();
            var startIndex = parameters[1].GetValue<int>();

            if (parameters.Length <= 2) return new ValueTask<ValueContainer>(new ValueContainer(str.Substring(startIndex)));
            
            
            var length = parameters[2].GetValue<int>();

            if (startIndex + length > str.Length)
            {
                throw new PowerAutomateMockUpException(
                    "The template language function 'substring' parameters are out of range: 'start index' " +
                    "and 'length' must be non-negative integers and their sum must be no larger than the length of " +
                    "the string.");
            }

            return new ValueTask<ValueContainer>(new ValueContainer(str.Substring(startIndex, length)));
        }
    }
}