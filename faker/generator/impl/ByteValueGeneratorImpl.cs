using System;

namespace faker.generator.impl
{
    public class ByteValueGeneratorImpl : AbstractValueGenerator
    {
        private readonly Random _random = new Random();

        public override Type Type => typeof(byte);

        public override object GenerateValue()
        {
            var random = new Random();
            var generatedByte = _random.Next(byte.MaxValue);
            return generatedByte;
        }
    }
}