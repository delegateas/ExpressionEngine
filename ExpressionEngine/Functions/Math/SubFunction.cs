using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Math
{
    public class SubFunction : Function
    {
        public SubFunction() : base("sub")
        {
        }

        /// <functionName>sub</functionName>
        /// <summary>
        /// Return the result from subtracting the second number from the first number.
        /// </summary>
        /// <definition>
        /// sub($ltminuend$gt, $ltsubtrahend$gt)
        /// </definition>
        /// <parameters>
        ///     <parameter name="$ltminuend$gt" required="Yes" type="Integer or Float">The number from which to subtract the subtrahend</parameter>
        ///     <parameter name="$ltsubtrahend$gt" required="Yes" type="Integer or Float">The number to subtract from the minuend</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>$ltresul$gt</value>
        ///     <type>Integer or Float</type>
        ///     <description>The result from subtracting the second number from the first number</description>
        /// </returns>
        /// <example>
        /// This example subtracts the second number from the first number:
        /// <code>
        /// sub(10.3, .3)
        /// </code>
        /// And returns this result: <c>10</c>
        /// </example>
        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidTemplateException(
                    "The template language function 'sub' expects two numeric parameters: " +
                    "the minuend as the first parameter and the subtrahend as the second parameter");
            }

            var first = parameters[0].GetValue<double>();
            var second = parameters[1].GetValue<double>();

            return new ValueTask<ValueContainer>(new ValueContainer(first - second));
        }
    }
}