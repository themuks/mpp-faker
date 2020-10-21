using System;

namespace faker.generator.impl
{
    public class IntegerValueGeneratorImpl : AbstractValueGenerator
    {
        private readonly Random _random = new Random();

        public override Type Type => typeof(int);

        public override object GenerateValue()
        {
            var generatedInteger = _random.Next();
            return generatedInteger;
        }
    }
}