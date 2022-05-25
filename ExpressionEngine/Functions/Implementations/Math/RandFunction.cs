using System;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Math
{
    public class RandFunction : IFunction
    {
        /// <functionName>rand</functionName>
        /// <summary>
        /// Return a random integer from a specified range, which is inclusive only at the starting end.
        /// </summary>
        /// <definition>
        /// rand($ltminValue$gt, $ltmaxValue$gt)
        /// </definition>
        /// <parameters>
        ///     <parameter def="$ltminValue$gt" required="Yes" type="Integer">The lowest integer in the range</parameter>
        ///     <parameter def="$ltmaxValue$gt" required="Yes" type="Integer">The integer that follows the highest integer in the range that the function can return</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>$ltproduct-resul$gt</value>
        ///     <type>Integer</type>
        ///     <description>The random integer returned from the specified range</description>
        /// </returns>
        /// <example>
        /// This example gets a random integer from the specified range, excluding the maximum value:
        /// <code>
        /// rand(1, 5)
        /// </code>
        /// And return these results:
        ///
        /// And returns one of these numbers as the result: <c>1</c>1, <c>2</c>, <c>3</c>, or <c>4</c>
        /// </example>
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidTemplateException(
                    "The template language function 'rand' expects two integer parameters: an inclusive minimum " +
                    "of the range as the first parameter and an inclusive maximum of the range as the second parameter");
            }

            var first = parameters[0].GetValue<int>();
            var second = parameters[1].GetValue<int>();

            var rand = new Random();

            return new ValueTask<ValueContainer>(new ValueContainer(rand.Next(first, second)));
        }
    }
}
