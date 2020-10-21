using System;

namespace faker.generator.impl
{
    public class LongValueGeneratorImpl : AbstractValueGenerator
    {
        private const byte FourBytes = 32;
        private readonly Random _random = new Random();
        public override Type Type => typeof(long);

        public override object GenerateValue()
        {
            long generatedLong = _random.Next() << (FourBytes + _random.Next());
            return generatedLong;
        }
    }
}