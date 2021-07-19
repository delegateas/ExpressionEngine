using System.Threading.Tasks;

namespace ExpressionEngine.Rules
{
    public class ConstantRule : IRule
    {
        private readonly ValueContainer _content;

        public ConstantRule(ValueContainer content)
        {
            _content = content;
        }

        public ValueTask<ValueContainer> Evaluate()
        {
            return new ValueTask<ValueContainer>(_content);
        }

        public string PrettyPrint()
        {
            return _content.Type() == ValueContainer.ValueType.String? $"'{_content}'" : _content.ToString();
        }
    }
}