using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Math
{
    [FunctionRegistration("range")]
    public class RangeFunction : IFunction
    {
        /// <functionName>range</functionName>
        /// <summary>
        /// Return an integer array that starts from a specified integer.
        /// </summary>
        /// <definition>
        /// range($ltstartIndex$gt, $ltcount$gt)
        /// </definition>
        /// <parameters>
        ///     <parameter def="$ltstartIndex$gt" required="Yes" type="Integer">An integer value that starts the array as the first item</parameter>
        ///     <parameter def="$ltcount$gt" required="Yes" type="Integer">The number of integers in the array. The <c>count</c> parameter value must be a positive integer that doesn't exceed 100,000.<br/><br/>Note: The sum of the <c>startIndex</c> and <c>count</c> values must not exceed 2,147,483,647.</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>[$ltrange-result$gt]</value>
        ///     <type>Array</type>
        ///     <description>The array with integers starting from the specified index</description>
        /// </returns>
        /// <example>
        /// This example creates an integer array that starts from the specified index and has the specified number of integers:
        /// <code>
        /// range(1, 4)
        /// </code>
        /// And returns this result: <c>[1, 2, 3, 4]</c>
        /// </example>
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
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
