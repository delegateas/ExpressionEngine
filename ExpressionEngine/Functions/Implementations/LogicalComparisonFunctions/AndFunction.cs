using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.LogicalComparisonFunctions
{
    [FunctionRegistration("and")]
    public class AndFunction : IFunction
    {
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            return new ValueTask<ValueContainer>(new ValueContainer(parameters.All(x => x.GetValue<bool>())));
        }
    }
}
