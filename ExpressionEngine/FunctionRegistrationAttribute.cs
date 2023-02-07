using System;

namespace ExpressionEngine
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class FunctionRegistrationAttribute : Attribute
    {
        /// <summary>
        /// Function Name
        /// </summary>
        public string FunctionName { get; }
        public Scope Scope { get; }

        /// <summary>
        /// Enable function to be discovered by <code>.WithFunctionDiscovery$lt{T}$gt</code> <see cref="ExpressionEngineDiExtensions"/>.
        /// Or to be added easily with .AddFunction;
        /// </summary>
        /// <param name="functionName">Name of function</param>
        /// <param name="scope">DI Scope - defaults to Transient</param>
        public FunctionRegistrationAttribute(string functionName, Scope scope = Scope.Transient)
        {
            FunctionName = functionName;
            Scope = scope;
        }
    }
}