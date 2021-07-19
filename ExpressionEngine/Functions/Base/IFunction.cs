﻿using System;
using System.Threading.Tasks;

namespace ExpressionEngine.Functions.Base
{
    public interface IFunction
    {
        string FunctionName { get; }
        ValueTask<ValueContainer> ExecuteFunction(ValueContainer[] strings);
    }

    public abstract class Function : IFunction
    {
        public string FunctionName { get; }

        protected Function(string functionName)
        {
            FunctionName = functionName ?? throw new ArgumentNullException(nameof(functionName));
        }

        public abstract ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters);
    }
}