using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.LogicalComparisonFunctions
{
    public class GreaterFunction : Function
    {
        public GreaterFunction() : base("greater")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentError("Too few arguments");
            }
            
            return new ValueTask<ValueContainer>(new ValueContainer(parameters[0].CompareTo(parameters[1]) > 0));
        }
    }
}