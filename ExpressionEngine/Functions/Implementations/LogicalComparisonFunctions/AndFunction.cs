using System.Linq;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.LogicalComparisonFunctions
{
    public class AndFunction : Function
    {
        public AndFunction() : base("and")
        {
        }

        public override ValueContainer ExecuteFunction(params ValueContainer[] parameters)
        {
            return new ValueContainer(parameters.All(x => x.GetValue<bool>()));
        }
    }
}