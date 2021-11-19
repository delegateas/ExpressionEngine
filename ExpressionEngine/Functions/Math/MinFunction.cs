using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Math
{
    public class MinFunction : Function
    {
        public MinFunction() : base("min")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length == 0)
            {
                throw new InvalidTemplateException(
                    "he template language function 'min' expects either an array of numbers or a comma " +
                    "separated list of numbers as its parameters. The function was invoked with no parameters");
            }

            if (parameters[0].Type() == ValueType.Array)
            {
                if (parameters.Length > 1)
                {
                    throw new InvalidTemplateException(
                        $"The template language function 'min' expects all of its parameters to be either " +
                        $"integer or decimal numbers. Found invalid parameter types: '{parameters[1].Type()}'.");
                }

                return new ValueTask<ValueContainer>(
                    new ValueContainer(parameters[0].GetValue<IEnumerable<ValueContainer>>().Min()));
            }

            var firstNonNumber =
                parameters.FirstOrDefault(x => x.Type() != ValueType.Float || x.Type() != ValueType.Integer);
            
            if (firstNonNumber != null)
            {
                throw new InvalidTemplateException(
                    $"The template language function 'min' expects all of its parameters to be either " +
                    $"integer or decimal numbers. Found invalid parameter types: '{firstNonNumber.Type()}'.");
            }

            return new ValueTask<ValueContainer>(parameters.Min());
        }
    }
}