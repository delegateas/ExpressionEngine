﻿using System;

namespace ExpressionEngine.Functions.CustomException
{
    public class ArgumentError : Exception
    {
        public ArgumentError()
        {

        }

        public ArgumentError(string msg) : base(msg)
        {

        }

        public ArgumentError(string msg, Exception inner) : base(msg, inner)
        {

        }
    }
}
