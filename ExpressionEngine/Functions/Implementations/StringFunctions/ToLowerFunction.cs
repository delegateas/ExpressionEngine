using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class ToLowerFunction : Function
    {
        public ToLowerFunction() : base("toLower")
        {
        }

        /// <functionName>toLower</functionName>
        /// <summary>
        /// Return a string in lowercase format. If a character in the string doesn't have a lowercase version, that character stays unchanged in the returned string.
        /// </summary>
        /// <definition>
        /// toLower('$lttext$gt')
        /// </definition>
        /// <parameters>
        ///     <parameter def="$lttext$gt" required="Yes" type="String">The string to return in lowercase format</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>$ltlowercase-text$gt</value>
        ///     <type>String</type>
        ///     <description>The original string in lowercase format</description>
        /// </returns>
        /// <example>
        /// This example converts this string to lowercase:
        /// <code>
        /// toLower('Hello World')
        /// </code>
        /// And returns this result: <c>"hello world"</c>
        /// </example>
        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentError(parameters.Length > 1 ? "Too many arguments" : "Too few arguments");
            }

            return new ValueTask<ValueContainer>(new ValueContainer(AuxiliaryMethods.VcIsString(parameters[0]).ToLower()));
        }
    }
}