using System;
using System.Text;

namespace faker.generator.impl
{
    public class StringValueGeneratorImpl : AbstractValueGenerator
    {
        private readonly Random _random = new Random();
        public override Type Type => typeof(string);

        public override object GenerateValue()
        {
            var stringLength = _random.Next(15) * 2;
            var temp = new byte[stringLength];
            _random.NextBytes(temp);
            var generatedString = Encoding.UTF8.GetString(temp);
            return generatedString;
        }
    }
}