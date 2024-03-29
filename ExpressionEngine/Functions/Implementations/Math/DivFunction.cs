﻿using System.Threading.Tasks;
using ExpressionEngine.Functions.Base;
using ExpressionEngine.Functions.CustomException;

namespace ExpressionEngine.Functions.Math
{
    public class DivFunction : IFunction
    {
        /// <functionName>div</functionName>
        /// <summary>
        /// Return the result from dividing two numbers. To get the remainder result, see <see cref="ModFunction"/>/>.
        /// </summary>
        /// <definition>
        /// add($ltsummand_1$gt, $ltsummand_2$gt)
        /// </definition>
        /// <parameters>
        ///     <parameter def="dividend" required="Yes" type="Integer or Float">The number to divide by the divisor</parameter>
        ///     <parameter def="divisor" required="Yes" type="Integer or Float">The number that divides the dividend, but cannot be 0</parameter>
        /// </parameters>
        /// <returns>
        ///     <value>$ltquotient-resultgt</value>
        ///     <type>Integer or Float</type>
        ///     <description>The result from dividing the first number by the second number. If either the dividend or divisor has Float type, the result has Float type.</description>
        /// </returns>
        /// <example>
        /// Both examples return this value with Integer type: <c>2</c>
        /// <code>
        /// div(10,5)
        /// div(11,5)
        /// </code>
        /// Both examples return this value with Float type: <c>2.2</c>
        /// <code>
        /// div(11,5.0)
        /// div(11.0,5)
        /// </code>
        /// </example>
        public ValueTask<ValueContainer> ExecuteFunction(params ValueContainer[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new InvalidTemplateException(
                    "The template language function 'div' expects two numeric parameters: " +
                    "the dividend as the first parameter and the divisor as the second parameter");
            }
            
            var first = parameters[0];
            var second = parameters[1];

            if (first.Type() == ValueType.Integer && second.Type() == ValueType.Integer)
            {
                return new ValueTask<ValueContainer>(
                    new ValueContainer(first.GetValue<int>() / second.GetValue<int>()));
            }

            if (first.IsNumber() && second.IsNumber())
            {
                return new ValueTask<ValueContainer>(
                    new ValueContainer(first.GetValue<decimal>() / second.GetValue<decimal>()));
            }

            throw new ExpressionEngineException($"Can only divide numbers, not {first.Type()} and {second.Type()}");
        }
    }
}
