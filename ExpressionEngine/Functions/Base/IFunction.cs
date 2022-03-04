using System.Threading.Tasks;

namespace ExpressionEngine.Functions.Base
{
    /// <summary>
    /// Interface for implementing a function
    /// </summary>
    public interface IFunction
    {
        /// <summary>
        /// Execute the given function implementation
        /// </summary>
        /// <param name="parameters">Parameters given to the functiopn</param>
        /// <returns>ValueContainer with the result</returns>
        ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters);
    }

    // public abstract class Function : IFunction
    // {
    //     public string FunctionName { get; }
    //
    //     protected Function(string functionName)
    //     {
    //         FunctionName = functionName ?? throw new ArgumentNullException(nameof(functionName));
    //     }
    //
    //     public abstract ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters);
    // }
}