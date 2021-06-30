﻿using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.LogicalComparisonFunctions
{
    public class EqualFunction : Function
    {
        public EqualFunction() : base("equal")
        {
        }

        public override ValueContainer ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentError("Too few arguments");
            }
            
            return new ValueContainer(parameters[0].Equals(parameters[1]));
        }
    }
}