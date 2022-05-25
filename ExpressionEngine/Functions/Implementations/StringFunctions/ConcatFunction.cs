using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class ConcatFunction : IFunction
    {
        /// <functionName>concat</functionName>
        /// <summary>
        /// Combine two or more strings, and return the combined string.
        /// </summary>
        /// <definition>
        /// concat('$lttext1$gt', '$lttext2$gt', ...)
        /// </definition>
        /// <parameters>
        ///     <parameter def="$lttext1$gt, $lttext2$gt, ..." required="Yes" type="String">At least two strings to combine</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>$lttext1$gt, $lttext2$gt, ...</value>
        ///     <type>String</type>
        ///     <description>The string created from the combined input strings. <br/><br/> Note: The length of the result must not exceed 104,857,600 characters.</description>
        /// </returns>
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentError("Too few arguments");
            }

            return new ValueTask<ValueContainer>(new ValueContainer(parameters.Aggregate("",
                (current, value) => current + value)));
        }
    }
}
