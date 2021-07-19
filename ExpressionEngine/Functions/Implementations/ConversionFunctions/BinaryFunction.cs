﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;

namespace ExpressionEngine.Functions.Implementations.ConversionFunctions
{
    public class BinaryFunction : Function
    {
        public BinaryFunction() : base("binary")
        {
        }

        public override ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            return parameters[0].Type() switch
            {
                ValueContainer.ValueType.String =>
                    new ValueTask<ValueContainer>(new ValueContainer(Encoding.UTF8
                        .GetBytes(parameters[0].GetValue<string>())
                        .Aggregate("", (s, b) => s + Convert.ToString(b, 2).PadLeft(8, '0')))),
                _ => throw new PowerAutomateMockUpException(
                    $"Array function can only operate on strings, not {parameters[0].Type()}.")
            };
        }
    }
}