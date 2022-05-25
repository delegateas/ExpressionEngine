using System.Collections.Generic;
using ExpressionEngine;
using ExpressionEngine.Functions.Math;

namespace Test.Expression
{
    public class MathFunctionTests
    {
        internal static object[] MathFunctionTestInput =
        {
            new object[]
            {
                new AddFunction(),
                "add",
                new[] {new ValueContainer(1), new ValueContainer(2)},
                new ValueContainer(3)
            },
            new object[]
            {
                new SubFunction(),
                "sub",
                new[] {new ValueContainer(1), new ValueContainer(2)},
                new ValueContainer(-1)
            },
            new object[]
            {
                new DivFunction(),
                "div",
                new[] {new ValueContainer(2), new ValueContainer(2)},
                new ValueContainer(1)
            },
            new object[]
            {
                new DivFunction(),
                "div",
                new[] {new ValueContainer(5), new ValueContainer(0.5)},
                new ValueContainer(10m)
            },
            new object[]
            {
                new MulFunction(),
                "mul",
                new[] {new ValueContainer(2), new ValueContainer(2)},
                new ValueContainer(4)
            },
            new object[]
            {
                new ModFunction(),
                "mod",
                new[] {new ValueContainer(5), new ValueContainer(4)},
                new ValueContainer(1)
            },
            new object[]
            {
                new RangeFunction(),
                "range",
                new[] {new ValueContainer(5), new ValueContainer(4)},
                new ValueContainer(new List<ValueContainer>
                    {new ValueContainer(5), new ValueContainer(6), new ValueContainer(7), new ValueContainer(8)})
            },
        };
    }
}