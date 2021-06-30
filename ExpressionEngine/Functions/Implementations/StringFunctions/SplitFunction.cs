﻿using System;
using System.Linq;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Implementations.StringFunctions
{
    public class SplitFunction : Function
    {
        public SplitFunction() : base("split")
        {
        }

        public override ValueContainer ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length < 2)
            {
                throw new ArgumentError("Too few arguments");
            }

            var str = AuxiliaryMethods.VcIsString(parameters[0]);
            var delimiter = AuxiliaryMethods.VcIsString(parameters[1]);

            return new ValueContainer(str.Split(new[] {delimiter}, StringSplitOptions.None)
                .Select(s => new ValueContainer(s)).ToArray());
        }
    }
}