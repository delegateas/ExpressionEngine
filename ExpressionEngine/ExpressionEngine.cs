using System.Threading.Tasks;

namespace ExpressionEngine
{
    public interface IExpressionEngine
    {
        ValueTask<string> Parse(string input);
        ValueTask<ValueContainer> ParseToValueContainer(string input);
    }

    public class ExpressionEngine : IExpressionEngine
    {
        private readonly ExpressionGrammar _expressionGrammar;

        public ExpressionEngine(ExpressionGrammar grammar)
        {
            _expressionGrammar = grammar;
        }

        public async ValueTask<string> Parse(string input)
        {
            return await _expressionGrammar.EvaluateToString(input);
        }

        public async ValueTask<ValueContainer> ParseToValueContainer(string input)
        {
            return await _expressionGrammar.EvaluateToValueContainer(input);
        }
    }
}