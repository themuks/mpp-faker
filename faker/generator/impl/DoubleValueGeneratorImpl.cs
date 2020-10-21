using System;

namespace faker.generator.impl
{
    public class DoubleValueGeneratorImpl : AbstractValueGenerator
    {
        private readonly Random _random = new Random();

        public override Type Type => typeof(double);

        public override object GenerateValue()
        {
            var generatedDouble = _random.NextDouble();
            return generatedDouble;
        }
    }
}