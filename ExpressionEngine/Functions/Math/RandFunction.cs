using System;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Math
{
    public class RandFunction : Function
    {
        public RandFunction() : base("rand")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidTemplateException(
                    "The template language function 'rand' expects two integer parameters: an inclusive minimum " +
                    "of the range as the first parameter and an inclusive maximum of the range as the second parameter");
            }

            var first = parameters[0].GetValue<int>();
            var second = parameters[1].GetValue<int>();

            var rand = new Random();

            return new ValueTask<ValueContainer>(new ValueContainer(rand.Next(first, second)));
        }
    }
}