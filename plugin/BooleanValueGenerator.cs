using System;
using System.Text;

namespace plugin
{
    public class BooleanValueGeneratorImpl : 
    {
        public override Type Type => typeof(bool);

        public override object GenerateValue()
        {
            var random = new Random();
            var randomNumber = random.Next();
            var mask = 0b1;
            var generatedBoolean = Convert.ToBoolean(randomNumber & mask);
            return generatedBoolean;
        }
    }
}