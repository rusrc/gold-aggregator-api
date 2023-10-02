﻿using System;

namespace GoldAggregator.Parser.Exceptions
{
    public class NormalizedValueException : Exception
    {
        public NormalizedValueException(string message) 
            : base(message)
        {
        }

        public NormalizedValueException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
