using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class ReplaceFunction : Function
    {
        public ReplaceFunction() : base("replace")
        {
        }

        
        /// <functionName>replace</functionName>
        /// <summary>
        /// Replace a substring with the specified string, and return the result string. This function is case-sensitive.
        /// </summary>
        /// <definition>
        /// replace('$lttext$gt', '$ltoldText$gt', '$ltnewText$gt')
        /// </definition>
        /// <parameters>
        ///     <parameter def="$lttext$gt" required="Yes" type="String">The string that has the substring to replace</parameter>
        ///     <parameter def="$ltoldText$gt" required="Yes" type="String">The substring to replace</parameter>
        ///     <parameter def="$ltnewtext$gt" required="Yes" type="String">The replacement string</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>$ltupdated-text$gt</value>
        ///     <type>String</type>
        ///     <description>The updated string after replacing the substring<br/>If the substring is not found, return the original string.</description>
        /// </returns>
        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 3)
            {
                throw new ArgumentError("Too few arguments");
            }

            var text = AuxiliaryMethods.VcIsString(parameters[0]);
            var oldStr = AuxiliaryMethods.VcIsString(parameters[1]);
            var newStr = AuxiliaryMethods.VcIsString(parameters[2]);

            return new ValueTask<ValueContainer>(new ValueContainer(text.Replace(oldStr, newStr)));
        }
    }
}