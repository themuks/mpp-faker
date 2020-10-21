using System;
using System.Collections.Generic;
using faker.entity;
using faker.entity.impl;
using faker.generator;
using NUnit.Framework;

namespace test
{
    [TestFixture]
    public class FakerTest
    {
        [OneTimeSetUp]
        public void Init()
        {
            _faker = new FakerImpl();
        }

        private IFaker _faker;

        private struct DefaultConstructorStruct
        {
            public int field1;
            private char field2;
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
        }

        private struct PrivateConstructorStruct
        {
            public int field1;
            private char field2;

            private PrivateConstructorStruct(char f)
            {
                field2 = f;
                field1 = 1;
            }
        }

        private struct MultipleConstructorStruct
        {
            private int field1;
            private char field2;
            public float field3;

            public MultipleConstructorStruct(int field)
            {
                field1 = field;
                field2 = '1';
                field3 = 0.1f;
            }

            public MultipleConstructorStruct(int field1, char field2, float field3)
            {
                this.field1 = field1;
                this.field2 = field2;
                this.field3 = field3;
            }
        }

        private class DefaultConstructorClass
        {
            public bool b;
            private double d;
            private float f;
            public int i;
            private long l;
            public string s;
            public DateTime t;
        }

        private class PrivateConstructorClass
        {
            public bool b;
            public char c;
            private double d;
            private float f;
            public int i;
            private long l;
            public string s;
            public DateTime t;

            private PrivateConstructorClass(float f, long l)
            {
                this.f = f;
                this.l = l;
            }
        }

        private class MultipleConstructorClass
        {
            private double d;
            private float f;
            public int i;
            private long l;
            public DateTime t;

            public MultipleConstructorClass()
            {
            }

            public MultipleConstructorClass(long l, double d)
            {
                this.l = l;
                this.d = d;
            }

            public string s { get; set; }
            public char c { get; }
            public bool b { get; }
        }

        private class CollectionClass
        {
            private List<char> chars;
            private List<double> doubles;
            public List<int> ints;
            public List<DateTime> times;

            public CollectionClass(List<double> doubles)
            {
                this.doubles = doubles;
            }
        }

        private class NestedClass
        {
            public int a;
            public CollectionClass c;
            private CollectionClass pc;
            public List<string> s;

            private NestedClass(CollectionClass pc)
            {
                this.pc = pc;
            }

            public NestedClass()
            {
            }
        }

        private class Circular1
        {
            public Circular2 C { get; set; }
        }

        private class Circular2
        {
            public Circular3 C { get; set; }
        }

        private class Circular3
        {
            public Circular1 C { get; set; }
        }

        [Test]
        public void CreatePrimitive()
        {
            var actual = _faker.Create<int>();
            Assert.GreaterOrEqual(actual, 0);
        }

        [Test]
        public void CreateDefaultConstructorStruct()
        {
            var actual = _faker.Create<DefaultConstructorStruct>();
            var notExpected = new DefaultConstructorStruct();
            Assert.AreNotEqual(notExpected.field1, actual.field1);
        }

        [Test]
        public void CreatePublicConstructorStruct()
        {
            var actual = _faker.Create<PublicConstructorStruct>();
            var notExpected = new PublicConstructorStruct('f');
            Assert.AreNotEqual(notExpected.field1, actual.field1);
        }

        [Test]
        public void CreatePrivateConstructorStruct()
        {
            var actual = _faker.Create<PrivateConstructorStruct>();
            var notExpected = new PrivateConstructorStruct();
            Assert.AreNotEqual(notExpected.field1, actual.field1);
        }

        [Test]
        public void CreateMultiConstructorStruct()
        {
            var actual = _faker.Create<MultipleConstructorClass>();
            var notExpected = new MultipleConstructorClass();
            Assert.AreEqual(notExpected.c, actual.c);
            Assert.AreNotEqual(notExpected.i, actual.i);
            Assert.AreNotEqual(notExpected.s, actual.s);
            Assert.AreNotEqual(notExpected.t, actual.t);
        }

        [Test]
        public void CreateDefaultConstructorClass()
        {
            var actual = _faker.Create<DefaultConstructorClass>();
            var notExpected = new DefaultConstructorClass();
            Assert.AreNotEqual(notExpected, actual);
        }

        [Test]
        public void CreatePrivateConstructorClass()
        {
            var actual = _faker.Create<PrivateConstructorClass>();
            PrivateConstructorClass expected = null;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateMultiConstructorClass()
        {
            var actual = _faker.Create<MultipleConstructorClass>();
            var notExpected = new MultipleConstructorClass();
            Assert.AreEqual(notExpected.c, actual.c);
            Assert.AreNotEqual(notExpected.i, actual.i);
            Assert.AreNotEqual(notExpected.s, actual.s);
            Assert.AreNotEqual(notExpected.t, actual.t);
        }

        [Test]
        public void CreateCollectionClass()
        {
            var actual = _faker.Create<CollectionClass>();
            var notExpected = new CollectionClass(new List<double>());
            CollectionAssert.AreNotEqual(notExpected.ints, actual.ints);
            CollectionAssert.AreNotEqual(notExpected.times, actual.times);
        }

        [Test]
        public void CreateNestedClass()
        {
            var actual = _faker.Create<NestedClass>();
            var notExpected = new NestedClass();
            Assert.AreNotEqual(notExpected.a, actual.a);
            Assert.AreNotEqual(notExpected.c, actual.c);
            CollectionAssert.AreNotEqual(notExpected.s, actual.s);
        }
    }
}