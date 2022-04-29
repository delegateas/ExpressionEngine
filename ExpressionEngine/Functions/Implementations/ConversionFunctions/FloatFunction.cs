using System;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.ConversionFunctions
{
    public class FloatFunction : IFunction
    {
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentError(parameters.Length > 1 ? "Too many arguments" : "Too few arguments");
            }

            switch (parameters[0].Type())
            {
                case ValueType.String:
                    if (decimal.TryParse(parameters[0].GetValue<string>(), out decimal doubleValue))
                    {
                        return new ValueTask<ValueContainer>(new ValueContainer(doubleValue));
                    }
                    else if (bool.TryParse(parameters[0].GetValue<string>(), out bool boolVal))
                    {
                        return boolVal
                            ? new ValueTask<ValueContainer>(new ValueContainer(1))
                            : new ValueTask<ValueContainer>(new ValueContainer(0));
                    }
                    else
                    {
                        throw new ExpressionEngineException(
                                   $"{parameters[0].GetValue<string>()} cannot be converted to float.");
                    }

                case ValueType.Integer:
                    return new ValueTask<ValueContainer>(new ValueContainer((decimal)parameters[0].GetValue<int>()));

                case ValueType.Float:
                    return new ValueTask<ValueContainer>(new ValueContainer(parameters[0].GetValue<decimal>()));

                case ValueType.Boolean:
                    return new ValueTask<ValueContainer>(new ValueContainer((decimal)(parameters[0].GetValue<bool>() ? 0 : 1)));

                default:
                    throw new ExpressionEngineException(
                        $"Float function can not operate on type: {parameters[0].Type()}.");
            }
        }
    }
}
