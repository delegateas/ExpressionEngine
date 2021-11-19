using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Math
{
    public class RangeFunction : Function
    {
        public RangeFunction() : base("range")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidTemplateException(
                    "The template language function 'range' expects two integer parameters: an inclusive start" +
                    " index of the range as the first parameter and a count of integers to return as the second parameter");
            }

            var startIndex = parameters[0].GetValue<int>();
            var count = parameters[1].GetValue<int>();

            if (count > 100000 || startIndex >= int.MaxValue - 100000)
            {
                throw new InvalidTemplateException(
                    "The template language function 'range' parameters are out of range: 'count' must be a positive " +
                    "integer no larger than '100000' and the sum of 'start index' and 'count' must be no larger than 2147483647");
            }

            return new ValueTask<ValueContainer>(
                new ValueContainer(Enumerable.Range(startIndex, count).Select(x => new ValueContainer(x))));
        }
    }
}