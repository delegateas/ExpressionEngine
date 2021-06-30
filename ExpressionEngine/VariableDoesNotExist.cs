using System;

namespace ExpressionEngine
{
    public class VariableDoesNotExists : Exception
    {
        public VariableDoesNotExists()
        {

        }

        public VariableDoesNotExists(string msg) : base(msg)
        {

        }

        public VariableDoesNotExists(string msg, Exception inner) : base(msg, inner)
        {

        }
    }
}
