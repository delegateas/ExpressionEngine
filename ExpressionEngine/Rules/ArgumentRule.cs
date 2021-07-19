using System.Threading.Tasks;

namespace ExpressionEngine.Rules
{
    public class ArgumentRule : IRule
    {
        private readonly ValueContainer _argument;

        public ArgumentRule(ValueContainer argument)
        {
            _argument = argument;
        }

        public ValueTask<ValueContainer> Evaluate()
        {
            return new ValueTask<ValueContainer>(_argument);
        }

        public string PrettyPrint()
        {
            return _argument.ToString();
        }
    }
}