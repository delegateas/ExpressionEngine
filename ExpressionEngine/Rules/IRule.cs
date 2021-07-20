using System.Threading.Tasks;

namespace ExpressionEngine.Rules
{
    public interface IRule
    {
        ValueTask<ValueContainer> Evaluate();

        string PrettyPrint();
    }
}