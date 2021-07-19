using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.LogicalComparisonFunctions
{
    public class AndFunction : Function
    {
        public AndFunction() : base("and")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            return new ValueTask<ValueContainer>(new ValueContainer(parameters.All(x => x.GetValue<bool>())));
        }
    }
}