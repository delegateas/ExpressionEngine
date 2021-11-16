using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.CollectionFunctions
{
    public class InterSectionFunction : Function
    {
        public InterSectionFunction() : base("intersection")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            return parameters[0].Type() switch
            {
                ValueType.Array => IntersectList(parameters),
                ValueType.Object => IntersectDict(parameters),
                _ => throw new ExpressionEngineException(
                    $"Can only intersect Array and Object, not {parameters[0].Type()}.")
            };
        }

        private ValueTask<ValueContainer> IntersectDict(IReadOnlyList<ValueContainer> parameters)
        {
            var first = parameters[0].GetValue<Dictionary<string, ValueContainer>>();

            return new ValueTask<ValueContainer>(new ValueContainer(parameters.Skip(1).Aggregate(first, ToDictionary)));
        }

        private Dictionary<string, ValueContainer> ToDictionary(Dictionary<string, ValueContainer> first,
            ValueContainer valueContainer)
        {
            var second = valueContainer.GetValue<Dictionary<string, ValueContainer>>();

            return first.Where(x => second.ContainsKey(x.Key))
                .ToDictionary(x => x.Key, x => second[x.Key]);
        }

        private ValueTask<ValueContainer> IntersectList(IReadOnlyList<ValueContainer> parameters)
        {
            var first = parameters[0].GetValue<IEnumerable<ValueContainer>>();

            var intersection = parameters.Skip(1)
                .Select(valueContainer => valueContainer.GetValue<IEnumerable<ValueContainer>>())
                .Aggregate(first, (current, collection) => current.Intersect(collection));

            return new ValueTask<ValueContainer>(new ValueContainer(intersection));
        }
    }
}