using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.ConversionFunctions
{
    public class BoolFunction : IFunction
    {
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            switch (parameters[0].Type())
            {
                case ValueType.String:
                    switch (parameters[0].GetValue<string>())
                    {
                        case "true":
                            return new ValueTask<ValueContainer>(new ValueContainer(true));
                        case "false":
                            return new ValueTask<ValueContainer>(new ValueContainer(false));
                        default:
                            throw new ExpressionEngineException(
                                $"Can only convert 'true' or 'false' to bool, not {parameters[0].GetValue<string>()}.");
                    }
                case ValueType.Integer:
                    var intValue = parameters[0].GetValue<int>();
                    return new ValueTask<ValueContainer>(new ValueContainer(intValue > 0 || intValue < 0));
                case ValueType.Float:
                    var doubleValue = parameters[0].GetValue<decimal>();
                    return new ValueTask<ValueContainer>(new ValueContainer(doubleValue > 0 || doubleValue < 0));
                case ValueType.Boolean:
                    return new ValueTask<ValueContainer>(parameters[0]);
                default:
                    throw new ExpressionEngineException(
                        $"Array function can only operate on strings, not {parameters[0].Type()}.");
            }
        }
    }
}
