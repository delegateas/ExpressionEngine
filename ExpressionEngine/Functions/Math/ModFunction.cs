using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Math
{
    public class ModFunction : Function
    {
        public ModFunction() : base("mod")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidTemplateException(
                    "The template language function 'mod' expects two numeric parameters: " +
                    "the dividend as the first parameter and the divisor as the second parameter");
            }

            var first = parameters[0].GetValue<double>();
            var second = parameters[1].GetValue<double>();

            return new ValueTask<ValueContainer>(new ValueContainer(first % second));
        }
    }
}