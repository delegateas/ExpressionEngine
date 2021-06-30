namespace ExpressionEngine.Rules
{
    public interface IRule
    {
        ValueContainer Evaluate();

        string PrettyPrint();
    }
}