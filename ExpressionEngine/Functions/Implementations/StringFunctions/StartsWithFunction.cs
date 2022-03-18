using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class StartsWithFunction : IFunction
    {
        /// <functionName>startsWith</functionName>
        /// <summary>
        /// Check whether a string starts with a specific substring. Return true when the substring is found, or return false when not found. This function is not case-sensitive.
        /// </summary>
        /// <definition>
        /// startsWith('$lttext$gt', '$ltsearchText$gt')
        /// </definition>
        /// <parameters>
        ///     <parameter def="$lttext$gt" required="Yes" type="String">The string to check</parameter>
        ///     <parameter def="$ltsearchText$gt" required="Yes" type="String">The starting string to find</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>true or false</value>
        ///     <type>Boolean</type>
        ///     <description>Return true when the starting substring is found. Return false when not found.</description>
        /// </returns>
        /// <example>
        /// This example checks whether the "hello world" string starts with the "hello" substring:
        /// <code>
        /// startsWith('hello world', 'hello')
        /// </code>
        /// And returns this result: <c>true</c>
        ///
        ///
        /// This example checks whether the "hello world" string starts with the "greetings" substring:
        /// <code>
        /// startsWith('hello world', 'greetings')
        /// </code>
        /// And returns this result: <c>false</c>
        /// </example>
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentError("Too few arguments");
            }

            return new ValueTask<ValueContainer>(new ValueContainer(AuxiliaryMethods.VcIsString(parameters[0])
                .StartsWith(AuxiliaryMethods.VcIsString(parameters[1]))));
        }
    }
}
