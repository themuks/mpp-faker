using System;
using faker.generator;

namespace char_generator
{
    public class CharacterValueGeneratorImpl : AbstractValueGenerator
    {
        private readonly Random _random = new Random();

        public override Type Type => typeof(char);

        public override object GenerateValue()
        {
            var generatedChar = (char) _random.Next(byte.MaxValue);
            return generatedChar;
        }
    }
}