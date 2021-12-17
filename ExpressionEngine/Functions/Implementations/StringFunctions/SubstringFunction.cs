using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class SubstringFunction : Function
    {
        public SubstringFunction() : base("substring")
        {
        }


        /// <functionName>substring</functionName>
        /// <summary>
        /// Return characters from a string, starting from the specified position, or index. Index values start with the number 0.
        /// </summary>
        /// <definition>
        /// substring('$lttext$gt', $ltstartIndex$gt, $ltlength$gt)
        /// </definition>
        /// <parameters>
        ///     <parameter name="$lttext$gt" required="Yes" type="String">The string whose characters you want</parameter>
        ///     <parameter name="$ltstartIndex$gt" required="Yes" type="Integer">A positive number equal to or greater than 0 that you want to use as the starting position or index value</parameter>
        ///     <parameter name="$ltlength$gt" required="Yes" type="Integer">A positive number of characters that you want in the substring</parameter>
        /// </parameters>
        /// <note>
        /// Make sure that the sum from adding the startIndex and length parameter values is less than the length of the string that you provide for the text parameter. Otherwise, you get an error, unlike similar functions in other languages where the result is the substring from the startIndex to the end of the string. The length parameter is optional and if not provided, the substring() function takes all the characters beginning from startIndex to the end of the string.
        /// </note>
        /// <returns>
        ///     <value>$ltsubstring-result$gt</value>
        ///     <type>String</type>
        ///     <description>A substring with the specified number of characters, starting at the specified index position in the source string</description>
        /// </returns>
        /// <example>
        /// This example creates a five-character substring from the specified string, starting from the index value 6:
        /// <code>
        /// substring('hello world', 6, 5)
        /// </code>
        /// And returns this result: <c>"world"</c>
        /// </example>
        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2 || parameters.Length > 3)
            {
                throw new ArgumentError(parameters.Length > 3 ? "Too many arguments" : "Too few arguments");
            }

            var str = parameters[0].GetValue<string>();
            var startIndex = parameters[1].GetValue<int>();

            if (parameters.Length <= 2)
                return new ValueTask<ValueContainer>(new ValueContainer(str.Substring(startIndex)));


            var length = parameters[2].GetValue<int>();

            if (startIndex + length > str.Length)
            {
                throw new ExpressionEngineException(
                    "The template language function 'substring' parameters are out of range: 'start index' " +
                    "and 'length' must be non-negative integers and their sum must be no larger than the length of " +
                    "the string.");
            }

            return new ValueTask<ValueContainer>(new ValueContainer(str.Substring(startIndex, length)));
        }
    }
}