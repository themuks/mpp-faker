using System;

namespace faker.generator
{
    public interface IValueGenerator
    {
        Type Type { get; }
        object GenerateValue();

        object GenerateList();
    }
}