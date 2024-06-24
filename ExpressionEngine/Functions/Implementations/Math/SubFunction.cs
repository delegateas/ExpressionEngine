using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Math
{
    [FunctionRegistration("sub")]
    public class SubFunction : IFunction
    {
        /// <functionName>sub</functionName>
        /// <summary>
        /// Return the result from subtracting the second number from the first number.
        /// </summary>
        /// <definition>
        /// sub($ltminuend$gt, $ltsubtrahend$gt)
        /// </definition>
        /// <parameters>
        ///     <parameter def="$ltminuend$gt" required="Yes" type="Integer or Float">The number from which to subtract the subtrahend</parameter>
        ///     <parameter def="$ltsubtrahend$gt" required="Yes" type="Integer or Float">The number to subtract from the minuend</parameter>
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
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidTemplateException(
                    "The template language function 'sub' expects two numeric parameters: " +
                    "the minuend as the first parameter and the subtrahend as the second parameter");
            }

            var first = parameters[0];
            var second = parameters[1];

            if (first.Type() == ValueType.Integer && second.Type() == ValueType.Integer)
            {
                return new ValueTask<ValueContainer>(
                    new ValueContainer(first.GetValue<int>() - second.GetValue<int>()));
            }

            if (first.IsNumber() && second.IsNumber())
            {
                return new ValueTask<ValueContainer>(
                    new ValueContainer(first.GetValue<decimal>() - second.GetValue<decimal>()));
            }

            throw new ExpressionEngineException($"Can only add numbers, not {first.Type()} and {second.Type()}");
        }
    }
}
