using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.ConversionFunctions
{
    public class BoolFunction : Function
    {
        public BoolFunction() : base("bool")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            switch (parameters[0].Type())
            {
                case ValueContainer.ValueType.String:
                    switch (parameters[0].GetValue<string>())
                    {
                        case "true":
                            return new ValueTask<ValueContainer>(new ValueContainer(true));
                        case "false":
                            return new ValueTask<ValueContainer>(new ValueContainer(false));
                        default:
                            throw new PowerAutomateMockUpException(
                                $"Can only convert 'true' or 'false' to bool, not {parameters[0].GetValue<string>()}.");
                    }
                case ValueContainer.ValueType.Integer:
                    var intValue = parameters[0].GetValue<int>();
                    return new ValueTask<ValueContainer>(new ValueContainer(intValue > 0 || intValue < 0));
                case ValueContainer.ValueType.Float:
                    var doubleValue = parameters[0].GetValue<double>();
                    return new ValueTask<ValueContainer>(new ValueContainer(doubleValue > 0 || doubleValue < 0));
                case ValueContainer.ValueType.Boolean:
                    return new ValueTask<ValueContainer>(parameters[0]);
                default:
                    throw new PowerAutomateMockUpException(
                        $"Array function can only operate on strings, not {parameters[0].Type()}.");
            }
        }
    }
}