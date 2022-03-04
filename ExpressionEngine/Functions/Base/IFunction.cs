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
}
