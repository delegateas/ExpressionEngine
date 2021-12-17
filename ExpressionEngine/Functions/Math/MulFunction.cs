using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Math
{
    public class MulFunction : Function
    {
        public MulFunction() : base("mul")
        {
        }

        /// <functionName>mul</functionName>
        /// <summary>
        /// Return the product from multiplying two numbers.
        /// </summary>
        /// <definition>
        /// mul($ltmultiplicand1$gt, $ltmultiplicand2$gt)
        /// </definition>
        /// <parameters>
        ///     <parameter name="$ltmultiplicand1$gt" required="Yes" type="Integer, Float, or mixed">The number to multiply by multiplicand2</parameter>
        ///     <parameter name="$ltmultiplicand1$gt" required="Yes" type="Integer, Float, or mixed">The number that multiples multiplicand1</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>$ltproduct-resul$gt</value>
        ///     <type>Integer or Float</type>
        ///     <description>The product from multiplying the first number by the second number</description>
        /// </returns>
        /// <example>
        /// These examples multiple the first number by the second number:
        /// <code>
        /// mul(1, 2)
        /// mul(1.5, 2)
        /// </code>
        /// And return these results:
        ///
        /// First example: <c>2</c>
        /// Second example <c>3</c>
        /// </example>
        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidTemplateException(
                    "The template language function 'mul' expects two numeric parameters: " +
                    "the first multiplicand as the first parameter and the second multiplicand as the second parameter");
            }

            var first = parameters[0].GetValue<double>();
            var second = parameters[1].GetValue<double>();

            return new ValueTask<ValueContainer>(new ValueContainer(first * second));
        }
    }
}