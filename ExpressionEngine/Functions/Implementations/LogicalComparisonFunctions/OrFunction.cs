using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.LogicalComparisonFunctions
{
    public class OrFunction : Function
    {
        public OrFunction() : base("or")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            return new ValueTask<ValueContainer>(new ValueContainer(parameters.Any(x => x.GetValue<bool>())));
        }
    }
}