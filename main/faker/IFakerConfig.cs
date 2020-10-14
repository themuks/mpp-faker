using System;

namespace main
{
    public interface IFakerConfig
    {
        void Add(Type type, IValueGenerator valueGenerator);

        IValueGenerator GetGenerator(Type type);

        bool ContainsTypeGenerator(Type type);
    }
}