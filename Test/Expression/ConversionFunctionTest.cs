using System.Collections.Generic;
using ExpressionEngine;
using ExpressionEngine.Functions.Implementations.ConversionFunctions;

namespace Test.Expression
{
    public class ConversionFunctionTest
    {
        internal static object[] ConversionFunctionTestInput =
        {
            new object[]
            {
                new ArrayFunction(),
                "array",
                new[] {new ValueContainer("string")},
                new ValueContainer(new[] {new ValueContainer("string")})
            },
            new object[]
            {
                new Base64Function(),
                "base64",
                new[] {new ValueContainer("hello")},
                new ValueContainer(new ValueContainer("aGVsbG8="))
            },
            new object[]
            {
                new Base64ToBinaryFunction(),
                "base64ToBinary",
                new[] {new ValueContainer("aGVsbG8=")},
                new ValueContainer(new ValueContainer("0110100001100101011011000110110001101111"))
            },
            new object[]
            {
                new Base64ToStringFunction(),
                "base64ToString",
                new[] {new ValueContainer("aGVsbG8=")},
                new ValueContainer(new ValueContainer("hello"))
            },
            new object[]
            {
                new BinaryFunction(),
                "binary",
                new[] {new ValueContainer("hello")},
                new ValueContainer(new ValueContainer("0110100001100101011011000110110001101111"))
            },
            new object[]
            {
                new BoolFunction(),
                "bool",
                new[] {new ValueContainer("true")},
                new ValueContainer(true)
            },
            new object[]
            {
                new BoolFunction(),
                "bool",
                new[] {new ValueContainer("false")},
                new ValueContainer(false)
            },
            new object[]
            {
                new BoolFunction(),
                "bool",
                new[] {new ValueContainer(0)},
                new ValueContainer(false)
            },
            new object[]
            {
                new BoolFunction(),
                "bool",
                new[] {new ValueContainer(1)},
                new ValueContainer(true)
            },
            new object[]
            {
                new BoolFunction(),
                "bool",
                new[] {new ValueContainer(-1)},
                new ValueContainer(true)
            },
            new object[]
            {
                new BoolFunction(),
                "bool",
                new[] {new ValueContainer(true)},
                new ValueContainer(true)
            },
            new object[]
            {
                new BoolFunction(),
                "bool",
                new[] {new ValueContainer(false)},
                new ValueContainer(false)
            },
            new object[]
            {
                new IntFunction(),
                "int",
                new[] {new ValueContainer("100")},
                new ValueContainer(100)
            },
            new object[]
            {
                new IntFunction(),
                "int",
                new[] {new ValueContainer("-120")},
                new ValueContainer(-120)
            },
            new object[]
            {
                new IntFunction(),
                "int",
                new[] {new ValueContainer(1.12345.ToString())},
                new ValueContainer(1)
            },
            new object[]
            {
                new IntFunction(),
                "int",
                new[] {new ValueContainer(1.66.ToString())},
                new ValueContainer(2)
            },
            new object[]
            {
                new IntFunction(),
                "int",
                new[] {new ValueContainer(-1)},
                new ValueContainer(-1)
            },
            new object[]
            {
                new IntFunction(),
                "int",
                new[] {new ValueContainer(1000000)},
                new ValueContainer(1000000)
            },
            new object[]
            {
                new IntFunction(),
                "int",
                new[] {new ValueContainer(false)},
                new ValueContainer(0)
            },
            new object[]
            {
                new IntFunction(),
                "int",
                new[] {new ValueContainer(true)},
                new ValueContainer(1)
            },
            new object[]
            {
                new IntFunction(),
                "int",
                new[] {new ValueContainer("true") },
                new ValueContainer(1)
            },
            new object[]
            {
                new IntFunction(),
                "int",
                new[] {new ValueContainer("false") },
                new ValueContainer(0)
            },
            new object[]
            {
                new FloatFunction(),
                "float",
                new[] {new ValueContainer("true")},
                new ValueContainer(1)
            },
            new object[]
            {
                new FloatFunction(),
                "float",
                new[] {new ValueContainer("false")},
                new ValueContainer(0)
            },
            new object[]
            {
                new FloatFunction(),
                "float",
                new[] {new ValueContainer(0)},
                new ValueContainer(0m)
            },
            new object[]
            {
                new FloatFunction(),
                "float",
                new[] {new ValueContainer(10005000)},
                new ValueContainer(10005000m)
            },
            new object[]
            {
                new FloatFunction(),
                "float",
                new[] {new ValueContainer(-10000078)},
                new ValueContainer(-10000078m)
            },
            new object[]
            {
                new FloatFunction(),
                "float",
                new[] {new ValueContainer(1.444444555)},
                new ValueContainer(1.444444555)
            },
            new object[]
            {
                new FloatFunction(),
                "float",
                new[] {new ValueContainer(-100.567890)},
                new ValueContainer(-100.56789)
            },
            new object[]
            {
                new FloatFunction(),
                "float",
                new[] {new ValueContainer(1.2534.ToString())},
                new ValueContainer(1.2534)
            },
            new object[]
            {
                new FloatFunction(),
                "float",
                new[] {new ValueContainer((-101500.56).ToString())},
                new ValueContainer(-101500.56)
            },
            new object[]
            {
                new CreateArrayFunction(),
                "createArray",
                new[] {new ValueContainer("hello"), new ValueContainer(1), new ValueContainer(true)},
                new ValueContainer(new List<ValueContainer>
                    {new ValueContainer("hello"), new ValueContainer(1), new ValueContainer(true)})
            },
            new object[]
            {
                new DataUriToBinaryFunction(),
                "dataUriToBinary",
                new[] {new ValueContainer("data:text/plain;charset=utf-8;base64,aGVsbG8=")},
                new ValueContainer("0110010001100001011101000110000100111010011101000110010101111000011101000010" +
                                   "111101110000011011000110000101101001011011100011101101100011011010000110000" +
                                   "101110010011100110110010101110100001111010111010101110100011001100010110100" +
                                   "1110000011101101100010011000010111001101100101001101100011010000101100011000" +
                                   "0101000111010101100111001101100010010001110011100000111101")
            }
        };
    }
}