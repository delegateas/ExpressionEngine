using System;

namespace ExpressionEngine
{
    internal class ExpressionEngineException : Exception
    {
        public ExpressionEngineException(string message) : base(message)
        {
        }
    }
}