using System;

namespace faker.generator.impl
{
    public class FloatValueGeneratorImpl : AbstractValueGenerator
    {
        private readonly Random _random = new Random();

        public override Type Type => typeof(float);

        public override object GenerateValue()
        {
            var generatedFloat = (float) _random.NextDouble();
            return generatedFloat;
        }
    }
}