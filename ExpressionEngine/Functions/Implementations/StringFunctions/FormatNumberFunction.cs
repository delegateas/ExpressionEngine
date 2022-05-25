using System.Globalization;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class FormatNumberFunction : IFunction
    {
        /// <functionName>formatNumber</functionName>
        /// <summary>
        /// Return a number as a string that's based on the specified format.
        /// </summary>
        /// <definition>
        /// formatNumber($ltnumber$gt, $ltformat$gt, $ltlocale$gt)
        /// </definition>
        /// <parameters>
        ///     <parameter def="$ltnumber$gt" required="Yes" type="Integer or Double">The value that you want to format.</parameter>
        ///     <parameter def="$ltformat$gt" required="Yes" type="String">A composite format string that specifies the format that you want to use. For the supported numeric format strings, see Standard numeric format strings, which are supported by number.ToString($ltformat$gt, $ltlocale$gt).</parameter>
        ///     <parameter def="$ltlocale$gt" required="Mo" type="String">The locale to use as supported by <c>number.ToString($ltformat$gt, $ltlocale$gt)</c>. If not specified, the default value is <c>en-us</c>.</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>$ltformatted-number$gt</value>
        ///     <type>String</type>
        ///     <description>The specified number as a string in the format that you specified. You can cast this return value to an <c>int</c> or <c>float</c>.</description>
        /// </returns>
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2 || parameters.Length > 3)
            {
                throw new ArgumentError(parameters.Length > 3 ? "Too many arguments" : "Too few arguments");
            }

            var value = parameters[0];
            var format = parameters[1].GetValue<string>();
            var locale = "en-us";
            if (parameters.Length > 2)
            {
                locale = parameters[2].GetValue<string>();
            }

            if (value.Type() == ValueType.Integer)
            {
                var intValue = value.GetValue<int>();

                return new ValueTask<ValueContainer>(new ValueContainer(intValue.ToString(format, CultureInfo.CreateSpecificCulture(locale))));
            }

            if (value.Type() == ValueType.Float)
            {
                var floatValue = value.GetValue<decimal>();

                return new ValueTask<ValueContainer>(new ValueContainer(floatValue.ToString(format, CultureInfo.CreateSpecificCulture(locale))));
            }

            throw new ExpressionEngineException("Expected an numeric value when formatting numbers.");
        }
    }
}
