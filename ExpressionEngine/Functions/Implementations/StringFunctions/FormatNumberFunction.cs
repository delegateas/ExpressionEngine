using System.Globalization;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class FormatNumberFunction : Function
    {
        public FormatNumberFunction() : base("formatNumber")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
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

            if (value.Type() == ValueContainer.ValueType.Integer)
            {
                var intValue = value.GetValue<int>();

                return new ValueTask<ValueContainer>(new ValueContainer(intValue.ToString(format, CultureInfo.CreateSpecificCulture(locale))));
            }

            if (value.Type() == ValueContainer.ValueType.Float)
            {
                var floatValue = value.GetValue<double>();

                return new ValueTask<ValueContainer>(new ValueContainer(floatValue.ToString(format, CultureInfo.CreateSpecificCulture(locale))));
            }

            throw new ExpressionEngineException("Expected an numeric value when formatting numbers.");
        }
    }
}