using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.CollectionFunctions
{
    public class ContainsFunction : IFunction
    {
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            var collection = parameters[0];
            var value = parameters[1];

            switch (collection.Type())
            {
                case ValueType.Array:
                    var array = collection.GetValue<IEnumerable<ValueContainer>>();

                    switch (value.Type())
                    {
                        case ValueType.Integer:
                        case ValueType.Float:
                        case ValueType.String:
                        case ValueType.Boolean:
                        case ValueType.Null:
                            return new ValueTask<ValueContainer>(new ValueContainer(array.Contains(value)));
                        case ValueType.Array:
                        case ValueType.Object:
                            return new ValueTask<ValueContainer>(new ValueContainer(false));
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                case ValueType.Object:
                    var key = value.GetValue<string>();
                    return new ValueTask<ValueContainer>(new ValueContainer(collection.AsDict().ContainsKey(key)));
                case ValueType.String:
                    var text = collection.GetValue<string>();
                    var substring = value.GetValue<string>();
                    return new ValueTask<ValueContainer>(new ValueContainer(text.Contains(substring)));
                default:
                    throw new ExpressionEngineException($"Cannot perform contains on {collection.Type()}.");
            }
        }
    }
}
