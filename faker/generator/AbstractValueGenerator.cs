using System;
using System.Collections;
using System.Collections.Generic;

namespace faker.generator
{
    public abstract class AbstractValueGenerator : IValueGenerator
    {
        public abstract Type Type { get; }

        public abstract object GenerateValue();

        public object GenerateList()
        {
            var type = typeof(List<>);
            type = type.MakeGenericType(Type);
            var list = (IList) Activator.CreateInstance(type);
            var random = new Random();
            var randomNumber = random.Next(10);
            for (var i = 0; i < randomNumber; i++) list.Add(GenerateValue());

            return list;
        }
    }
}