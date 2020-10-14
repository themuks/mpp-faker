using System;

namespace main.impl
{
    public class IntegerValueGeneratorImpl : IValueGenerator
    {
        public object GenerateValue()
        {
            Random random = new Random();
            return random.Next();
        }
    }
}