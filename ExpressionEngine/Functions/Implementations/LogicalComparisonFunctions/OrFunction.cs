using System.Linq;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.LogicalComparisonFunctions
{
    public class OrFunction : Function
    {
        public OrFunction() : base("or")
        {
        }

        public override ValueContainer ExecuteFunction(params ValueContainer[] parameters)
        {
            return new ValueContainer(parameters.Any(x => x.GetValue<bool>()));
        }
    }
}