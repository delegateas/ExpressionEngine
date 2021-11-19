using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Math
{
    public class DivFunction : Function
    {
        public DivFunction() : base("div")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidTemplateException(
                    "The template language function 'div' expects two numeric parameters: " +
                    "the dividend as the first parameter and the divisor as the second parameter");
            }

            var first = parameters[0].GetValue<double>();
            var second = parameters[1].GetValue<double>();

            if (second == 0)
            {
                throw new ExpressionEngineException(
                    "Attempt to divide an integral or decimal value by zero in function 'div'.");
            }

            return new ValueTask<ValueContainer>(new ValueContainer(first / second));
        }
    }
}