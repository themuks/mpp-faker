using System;
using System.Collections.Generic;
using faker.entity;
using faker.entity.impl;

namespace main
{
    public class Program
    {
        private static void Main(string[] args)
        {
            IFaker faker = new FakerImpl();
            var collectionClass = faker.Create<CollectionClass>();
            Console.WriteLine(collectionClass);
            var simpleConstructorClass = faker.Create<SimpleConstructorClass>();
            Console.WriteLine(simpleConstructorClass);
            faker.MaxCircularDepth = 1;
            var circular1 = faker.Create<Circular1>();
            Console.WriteLine(circular1);
            var nestedClass = faker.Create<NestedClass>();
            Console.WriteLine(nestedClass);
            var publicConstructorClass = faker.Create<PublicConstructorStruct>();
            Console.WriteLine(publicConstructorClass);
            var dateTime = faker.Create<DateTime>();
            Console.WriteLine(dateTime);
            var privateSetClass = faker.Create<PrivateSetClass>();
            Console.WriteLine(privateSetClass);
            var s = faker.Create<string>();
            Console.WriteLine(s);
            var simpleStruct = faker.Create<SimpleStruct>();
            Console.WriteLine(simpleStruct);
        }

        private class SimpleConstructorClass
        {
            public bool b;
            private char c;
            private double d;
            public string i;
            public DateTime t;

            public SimpleConstructorClass(double d)
            {
                this.d = d;
            }

            public override string ToString()
            {
                return $"{nameof(c)}: {c}, {nameof(b)}: {b}, {nameof(d)}: {d}, {nameof(i)}: {i}, {nameof(t)}: {t}";
            }
        }

        private class CollectionClass
        {
            public List<char> chars;
            private List<double> doubles;
            public List<int> ints;

            public CollectionClass(List<double> doubles)
            {
                this.doubles = doubles;
            }

            public override string ToString()
            {
                return $"{nameof(chars)}: {chars}, {nameof(doubles)}: {doubles}, {nameof(ints)}: {ints}";
            }
        }

        private class NestedClass
        {
            public int a;
            public CollectionClass c;
            private SimpleConstructorClass pc;
            public List<long> s;

            public NestedClass(SimpleConstructorClass pc)
            {
                this.pc = pc;
            }

            public override string ToString()
            {
                return $"{nameof(a)}: {a}, {nameof(c)}: {c}, {nameof(pc)}: {pc}, {nameof(s)}: {s}";
            }
        }

        private class Circular1
        {
            public Circular2 c { get; set; }

            public override string ToString()
            {
                return $"{nameof(c)}: {c}";
            }
        }

        private class Circular2
        {
            public Circular3 c { get; set; }

            public override string ToString()
            {
                return $"{nameof(c)}: {c}";
            }
        }

        private class Circular3
        {
            public Circular1 c { get; set; }

            public override string ToString()
            {
                return $"{nameof(c)}: {c}";
            }
        }

        private struct SimpleStruct
        {
            public string field1;
            private char field2;

            public override string ToString()
            {
                return $"{nameof(field1)}: {field1}, {nameof(field2)}: {field2}";
            }
        }

        private class PrivateSetClass
        {
            public PrivateSetClass(int prop)
            {
                this.prop = prop;
            }

            public int prop { get; }
            public long prop2 { get; set; }

            public override string ToString()
            {
                return $"{nameof(prop)}: {prop}, {nameof(prop2)}: {prop2}";
            }
        }

        private struct PublicConstructorStruct
        {
            public int field1;
            private char field2;

            public PublicConstructorStruct(char f)
            {
                field2 = f;
                field1 = 1;
            }

            public override string ToString()
            {
                return $"{nameof(field1)}: {field1}, {nameof(field2)}: {field2}";
            }
        }
    }
}