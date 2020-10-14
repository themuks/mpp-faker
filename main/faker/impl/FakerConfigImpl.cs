using System;
using System.Collections.Generic;
using main.impl;

namespace main
{
    public class FakerConfigImpl : IFakerConfig
    {
        private Dictionary<Type, IValueGenerator> _dictionary = new Dictionary<Type, IValueGenerator>();

        public FakerConfigImpl()
        {
            _dictionary.Add(typeof(int), new IntegerValueGeneratorImpl());
        }
        
        public void Add(Type type, IValueGenerator valueGenerator)
        {
            _dictionary.Add(type, valueGenerator);
        }

        public IValueGenerator GetGenerator(Type type)
        {
            return _dictionary[type];
        }

        public bool ContainsTypeGenerator(Type type)
        {
            return _dictionary.ContainsKey(type);
        }
    }
}