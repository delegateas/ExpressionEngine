using System;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.ConversionFunctions
{
    public class FloatFunction : Function
    {
        public FloatFunction() : base("float")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length == 0)
            {
                throw InvalidTemplateException.BuildInvalidLanguageFunction("SomeAction", "float");
            }

            switch (parameters[0].Type())
            {
                case ValueType.String:
                    if (Double.TryParse(parameters[0].GetValue<string>(), out double doubleValue))
                    {
                        return new ValueTask<ValueContainer>(new ValueContainer(doubleValue));
                    }
                    else if (Boolean.TryParse(parameters[0].GetValue<string>(), out bool boolVal))
                    {
                        if (boolVal)
                            return new ValueTask<ValueContainer>(new ValueContainer(1));
                        else
                            return new ValueTask<ValueContainer>(new ValueContainer(0));
                    }
                    else
                    {
                        throw new ExpressionEngineException(
                                   $"{parameters[0].GetValue<string>()} cannot be converted to float.");
                    }

                case ValueType.Integer:
                    return new ValueTask<ValueContainer>(new ValueContainer((float)parameters[0].GetValue<int>()));

                case ValueType.Float:
                    return new ValueTask<ValueContainer>(new ValueContainer(parameters[0].GetValue<double>()));

                case ValueType.Boolean:
                    return new ValueTask<ValueContainer>(new ValueContainer((double)(parameters[0].GetValue<bool>() ? 0 : 1)));

                default:
                    throw new ExpressionEngineException(
                        $"Float function can not operate on type: {parameters[0].Type()}.");
            }
        }
    }
}