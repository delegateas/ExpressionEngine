using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.LogicalComparisonFunctions
{
    public class IfFunction : Function
    {
        public IfFunction() : base("if")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            return new ValueTask<ValueContainer>(parameters[0].GetValue<bool>() ? parameters[1] : parameters[2]);
        }
    }
}