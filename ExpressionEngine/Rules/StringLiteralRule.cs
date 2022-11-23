using System.Threading.Tasks;

namespace ExpressionEngine.Rules
{
    /// <inheritdoc />
    public class StringLiteralRule : IRule
    {
        private readonly ValueContainer _content;

        /// <summary>
        /// Represents string literals, i.e. <code>'Hello World!'</code>
        /// </summary>
        /// <param name="content">String value</param>
        public StringLiteralRule(ValueContainer content)
        {
            _content = content;
        }

        /// <inheritdoc />
        public ValueTask<ValueContainer> Evaluate()
        {
            return new ValueTask<ValueContainer>(_content);
        }

        /// <inheritdoc />
        public string PrettyPrint()
        {
            return $"'{_content}'";
        }
    }
}