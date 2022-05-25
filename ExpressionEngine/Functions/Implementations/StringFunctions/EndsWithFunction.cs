using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class EndsWithFunction : IFunction
    {
        /// <functionName>endsWith</functionName>
        /// <summary>
        /// Check whether a string ends with a specific substring. Return true when the substring is found, or return false when not found. This function is not case-sensitive.
        /// </summary>
        /// <definition>
        /// endsWith('$lttext$gt', '$ltsearchText$gt')
        /// </definition>
        /// <parameters>
        ///     <parameter def="$lttext$gt" required="Yes" type="String">The string to check</parameter>
        ///     <parameter def="$lttext$gt" required="Yes" type="String">The ending substring to find</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>true or false</value>
        ///     <type>Boolean</type>
        ///     <description>Return true when the ending substring is found. Return false when not found.</description>
        /// </returns>
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentError("Too few arguments");
            }

            return new ValueTask<ValueContainer>(new ValueContainer(AuxiliaryMethods.VcIsString(parameters[0])
                .EndsWith(AuxiliaryMethods.VcIsString(parameters[1]))));
        }
    }
}
