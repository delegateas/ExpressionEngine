using System;
using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class SplitFunction : IFunction
    {
        /// <functionName>split</functionName>
        /// <summary>
        /// Return an array that contains substrings, separated by commas, based on the specified delimiter character in the original string.
        /// </summary>
        /// <definition>
        /// split('$lttext$gt', '$ltdelimiter$gt')
        /// </definition>
        /// <parameters>
        ///     <parameter def="$lttext$gt" required="Yes" type="String">The string to separate into substrings based on the specified delimiter in the original string</parameter>
        ///     <parameter def="$lttext$gt" required="Yes" type="String">The character in the original string to use as the delimiter</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>[$ltsubstring1$gt,$ltsubstring2$gt,...]</value>
        ///     <type>Array</type>
        ///     <description>An array that contains substrings from the original string, separated by commas</description>
        /// </returns>
        // [ArguementAttribute()]
        // [ArguementAttribute()]
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentError("Too few arguments");
            }

            var str = AuxiliaryMethods.VcIsString(parameters[0]);
            var delimiter = AuxiliaryMethods.VcIsString(parameters[1]);

            return new ValueTask<ValueContainer>(new ValueContainer(str.Split(new[] {delimiter}, StringSplitOptions.None)
                .Select(s => new ValueContainer(s)).ToArray()));
        }
    }
}
