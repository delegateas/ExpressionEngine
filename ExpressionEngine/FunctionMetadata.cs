using System;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine
{
    /// <summary>
    /// FunctionMetadata is used to register functions
    /// </summary>
    public class FunctionMetadata
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="function">Function must implement <see cref="IFunction"/></param>
        /// <param name="alias"></param>
        public FunctionMetadata(Type function, string alias)
        {
            
            Function = function;
            Alias = alias;
        }

        /// <summary>
        /// Function implementation
        /// </summary>
        public Type Function { get; set; }

        /// <summary>
        /// Alias used to invoke function
        /// </summary>
        public string Alias { get; set; }
    }
}