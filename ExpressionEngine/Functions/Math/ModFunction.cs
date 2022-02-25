using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Math
{
    public class ModFunction : Function
    {
        public ModFunction() : base("mod")
        {
        }

        /// <functionName>mod</functionName>
        /// <summary>
        /// Return the remainder from dividing two numbers. To get the integer result, <see cref="DivFunction"/>.
        /// </summary>
        /// <definition>
        /// mod($ltdividend$gt, $ltdivisor$gt)
        /// </definition>
        /// <parameters>
        ///     <parameter def="$ltdividend$gt" required="Yes" type="Integer or Float">The number to divide by the divisor</parameter>
        ///     <parameter def="$ltdivisor$gt" required="Yes" type="Integer or Float">The number that divides the dividend, but cannot be 0.</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>$ltmodulo-result$gt</value>
        ///     <type>Integer or Float</type>
        ///     <description>The remainder from dividing the first number by the second number</description>
        /// </returns>
        /// <example>
        /// This example divides the first number by the second number:
        /// <code>
        /// mod(3, 2)
        /// </code>
        /// And returns this result: <c>1</c>
        ///
        /// This example shows that if one or both values are negative, the result matches the sign of the dividend:
        /// <code>
        /// mod(-5, 2)
        /// mod(4, -3)
        /// </code>
        /// The example returns these results:
        ///
        /// First example: <c>-1</c>
        /// Second example: <c>1</c>
        /// </example>
        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidTemplateException(
                    "The template language function 'mod' expects two numeric parameters: " +
                    "the dividend as the first parameter and the divisor as the second parameter");
            }

            var first = parameters[0].GetValue<double>();
            var second = parameters[1].GetValue<double>();

            return new ValueTask<ValueContainer>(new ValueContainer(first % second));
        }
    }
}