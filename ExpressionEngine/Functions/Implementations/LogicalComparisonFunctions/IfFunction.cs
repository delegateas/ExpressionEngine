using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.LogicalComparisonFunctions
{
    [FunctionRegistration("if")]
    public class IfFunction : IFunction
    {
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            return new ValueTask<ValueContainer>(parameters[0].GetValue<bool>() ? parameters[1] : parameters[2]);
        }
    }
}
