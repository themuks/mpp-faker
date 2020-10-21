using System;
using faker.generator;

namespace boolean_generator
{
    public class BooleanValueGeneratorImpl : AbstractValueGenerator
    {
        private readonly Random _random = new Random();

        public override Type Type => typeof(bool);

        public override object GenerateValue()
        {
            var randomNumber = _random.Next();
            const int mask = 0b1;
            var generatedBoolean = Convert.ToBoolean(randomNumber & mask);
            return generatedBoolean;
        }
    }
}