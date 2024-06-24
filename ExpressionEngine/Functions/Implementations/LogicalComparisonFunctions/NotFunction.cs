using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.LogicalComparisonFunctions
{
    [FunctionRegistration("not")]
    public class NotFunction : IFunction
    {
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 1)
            {
                throw new ArgumentError("Too few arguments");
            }
            
            return new ValueTask<ValueContainer>(new ValueContainer(!parameters[0].GetValue<bool>()));
        }
    }
}
