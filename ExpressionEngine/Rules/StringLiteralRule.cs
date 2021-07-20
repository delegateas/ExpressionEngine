using System.Threading.Tasks;

namespace ExpressionEngine.Rules
{
    public class StringLiteralRule : IRule
    {
        private readonly ValueContainer _content;

        public StringLiteralRule(ValueContainer content)
        {
            _content = content;
        }

        public ValueTask<ValueContainer> Evaluate()
        {
            return new ValueTask<ValueContainer>(_content);
        }

        public string PrettyPrint()
        {
            return $"'{_content}'";
        }
    }
}