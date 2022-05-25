using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class ToUpperFunction : IFunction
    {
        /// <functionName>toUpper</functionName>
        /// <summary>
        /// Return a string in uppercase format. If a character in the string doesn't have an uppercase version, that character stays unchanged in the returned string.
        /// </summary>
        /// <definition>
        /// toUpper('$lttext$gt')
        /// </definition>
        /// <parameters>
        ///     <parameter def="$lttext$gt" required="Yes" type="String">The string to return in uppercase format</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>$ltuppercase-text$gt</value>
        ///     <type>String</type>
        ///     <description>The original string in uppercase format</description>
        /// </returns>
        /// <example>
        /// This example converts this string to uppercase:
        /// <code>
        /// toLower('Hello World')
        /// </code>
        /// And returns this result: <c>"HELLO WORLD"</c>
        /// </example>
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentError(parameters.Length > 1 ? "Too many arguments" : "Too few arguments");
            }

            return new ValueTask<ValueContainer>(new ValueContainer(AuxiliaryMethods.VcIsString(parameters[0]).ToUpper()));
        }
    }
}
