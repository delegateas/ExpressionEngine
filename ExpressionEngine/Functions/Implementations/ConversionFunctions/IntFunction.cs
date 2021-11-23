using System;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.ConversionFunctions
{
    public class IntFunction : Function
    {
        public IntFunction() : base("int")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            switch (parameters[0].Type())
            {
                case ValueType.String:
                    if (Int32.TryParse(parameters[0].GetValue<string>(), out int intValue))
                    {
                        CheckIntMaxOrMin(intValue);
                        return new ValueTask<ValueContainer>(new ValueContainer(intValue));
                    }
                    else if (Double.TryParse(parameters[0].GetValue<string>(), out double doubleValue))
                    {
                        var intVal = (int)System.Math.Round(doubleValue);
                        CheckIntMaxOrMin(intVal);
                        return new ValueTask<ValueContainer>(new ValueContainer(intVal));
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
                                   $"{parameters[0].GetValue<string>()} cannot be converted to integer.");
                    }

                case ValueType.Integer:
                    var val = parameters[0].GetValue<int>();
                    CheckIntMaxOrMin(val);
                    return new ValueTask<ValueContainer>(new ValueContainer(parameters[0].GetValue<int>()));

                case ValueType.Float:
                    var floatToIntVal = (int)System.Math.Round(parameters[0].GetValue<double>());
                    CheckIntMaxOrMin(floatToIntVal);
                    return new ValueTask<ValueContainer>(new ValueContainer((int)System.Math.Round(parameters[0].GetValue<double>())));

                case ValueType.Boolean:
                    return new ValueTask<ValueContainer>(new ValueContainer(parameters[0].GetValue<bool>() ? 1 : 0));

                default:
                    throw new ExpressionEngineException(
                        $"Int function can not operate on type: {parameters[0].Type()}.");
            }
        }

        private void CheckIntMaxOrMin(int val)
        {
            if (val == int.MinValue)
                throw new ExpressionEngineException(
                        $"Input is less than minimum value of int {int.MinValue}");
            else if (val == int.MaxValue)
                throw new ExpressionEngineException(
                        $"Input is greater than maximum value of int: {int.MaxValue}");
        }
    }
}