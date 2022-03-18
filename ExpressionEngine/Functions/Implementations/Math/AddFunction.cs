using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Math
{
    public class AddFunction : IFunction
    {
        /// <functionName>add</functionName>
        /// <summary>
        /// Return the result from adding two numbers.
        /// </summary>
        /// <definition>
        /// add($ltsummand_1$gt, $ltsummand_2$gt)
        /// </definition>
        /// <parameters>
        ///     <parameter def="$ltsummand_1$gt, $ltsummand_2$gt" required="Yes" type="Integer, Float, or mixed">The numbers to add</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>$ltresult-sum$gt</value>
        ///     <type>Integer or Float</type>
        ///     <description>The result from adding the specified numbers</description>
        /// </returns>
        /// <example>
        /// This example adds the specified numbers:
        /// <code>
        /// add(1, 1.5)
        /// </code>
        /// And returns this result: <c>2.5</c>
        /// </example>
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidTemplateException(
                    "The template language function 'add' expects two numeric parameters: " +
                    "the first summand as the first parameter and the second summand as the second parameter.");
            }

            var first = parameters[0].GetValue<double>();
            var second = parameters[1].GetValue<double>();

            return new ValueTask<ValueContainer>(new ValueContainer(first + second));
        }
    }
}
