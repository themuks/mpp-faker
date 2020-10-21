using System;

namespace faker.generator.impl
{
    public class ShortValueGeneratorImpl : AbstractValueGenerator
    {
        private readonly Random _random = new Random();

        public override Type Type => typeof(short);

        public override object GenerateValue()
        {
            int generatedShort = (short) _random.Next();
            return generatedShort;
        }
    }
}