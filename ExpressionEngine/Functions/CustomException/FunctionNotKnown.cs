﻿using System;

namespace ExpressionEngine.Functions.CustomException
{
    public class FunctionNotKnown : Exception
    {
        public FunctionNotKnown()
        {

        }

        public FunctionNotKnown(string msg) : base(msg)
        {

        }

        public FunctionNotKnown(string msg, Exception inner) : base(msg, inner)
        {

        }
    }
}
