using System;

namespace faker.generator.impl
{
    public class DateTimeValueGeneratorImpl : AbstractValueGenerator
    {
        private readonly Random _random = new Random();

        public override Type Type => typeof(DateTime);

        public override object GenerateValue()
        {
            var start = new DateTime(1995, 1, 1);
            var range = (DateTime.Today - start).Days;
            var dayCount = _random.Next(range);
            var generatedDateTime = start.AddDays(dayCount);
            return generatedDateTime;
        }
    }
}