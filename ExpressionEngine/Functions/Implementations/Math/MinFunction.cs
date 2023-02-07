using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Math
{
    [FunctionRegistration("min")]
    public class MinFunction : IFunction
    {
        /// <functionName>min</functionName>
        /// <summary>
        /// Return the lowest value from a list or array with numbers that is inclusive at both ends.
        /// </summary>
        /// <definition>
        /// min($ltnumber1$gt, $ltnumber2$gt, ...)
        /// min([$ltnumber1$gt, $ltnumber2$gt, ...])
        /// </definition>
        /// <parameters>
        ///     <parameter def="$ltnumber_1$gt, $ltnumber_2$gt,..." required="Yes" type="Integer, Float, or both">The set of numbers from which you want the lowest value</parameter>
        ///     <parameter def="$[ltnumber_1$gt, $ltnumber_2$gt, ...]" required="Yes" type="Array - Integer, Float, or both">The array of numbers from which you want the lowest value</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>$ltmin-sum$gt</value>
        ///     <type>Integer or Float</type>
        ///     <description>The lowest value in the specified set of numbers or specified array</description>
        /// </returns>
        /// <example>
        /// These examples get the highest value from the set of numbers and the array:
        /// <code>
        /// min(1, 2, 3)
        /// min(createArray(1, 2, 3))
        /// </code>
        /// And return this result: <c>1</c>
        /// </example>
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length == 0)
            {
                throw new InvalidTemplateException(
                    "he template language function 'min' expects either an array of numbers or a comma " +
                    "separated list of numbers as its parameters. The function was invoked with no parameters");
            }

            if (parameters[0].Type() == ValueType.Array)
            {
                if (parameters.Length > 1)
                {
                    throw new InvalidTemplateException(
                        $"The template language function 'min' expects all of its parameters to be either " +
                        $"integer or decimal numbers. Found invalid parameter types: '{parameters[1].Type()}'.");
                }

                return new ValueTask<ValueContainer>(
                    new ValueContainer(parameters[0].GetValue<IEnumerable<ValueContainer>>().Min()));
            }

            var firstNonNumber =
                parameters.FirstOrDefault(x => x.Type() != ValueType.Float || x.Type() != ValueType.Integer);
            
            if (firstNonNumber != null)
            {
                throw new InvalidTemplateException(
                    $"The template language function 'min' expects all of its parameters to be either " +
                    $"integer or decimal numbers. Found invalid parameter types: '{firstNonNumber.Type()}'.");
            }

            return new ValueTask<ValueContainer>(parameters.Min());
        }
    }
}
