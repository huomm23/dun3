﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Hertkorn.Framework.FilterByExample;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Com.Hertkorn.Framework.Querycache
{
    [TestFixture]
    public class QuerycacheFilterByExampleTest
    {
        #region SetUp

        [SetUp]
        public void SetUp()
        {
            m_example = new TestClass("test0", 1, 2, 6);

            m_exampleQueryable = new List<TestClass>()
            {
                new TestClass("test0", 1, 2, 6),
                new TestClass("test0", 2, 3, 7),
                new TestClass("test0", 3, 4, 8),
                new TestClass("test1", 4, 3, 9),
                new TestClass("test1", 5, 3, 9),
                new TestClass("test1", 6, 3, 9),
                new TestClass("test1", 7, 3, 9),
                new TestClass("test1", 8, 3, 9),
                new TestClass("test0", 9, 3, 9),
                new TestClass("test0", 9, 3, 9),
                new TestClass("test0", 9, 3, 9),
                new TestClass("test0", 9, 3, 9),
                new TestClass("test0", 9, 3, 9),
                new TestClass("test1", 10, 3, 9),
                new TestClass("test0", 11, 3, 9),
                new TestClass("test1", 12, 3, 9),
                new TestClass("test1", 13, 3, 9),
                new TestClass("test0", 14, 3, 9),
                new TestClass("test1", 15, 3, 9),
                new TestClass("test1", 16, 3, 9),
                new TestClass("test0", 17, 3, 9),
                new TestClass("test0", 18, 3, 9),
                new TestClass("test1", 19, 3, 9),
                new TestClass("test0", 10, 3, 9),
                new TestClass("test1", 11, 3, 9),
                new TestClass("test1", 12, 3, 9),
                new TestClass("test0", 13, 3, 9),
                new TestClass("test1", 14, 3, 9),
                new TestClass("test1", 15, 3, 9),
                new TestClass("test0", 16, 3, 9),
                new TestClass("test1", 17, 3, 9),
                new TestClass("test1", 18, 3, 9),
                new TestClass("test0", 19, 3, 9),
                new TestClass("test1", 20, 3, 9),
                new TestClass("test0", 11, 3, 9),
                new TestClass("test0", 12, 3, 9),
                new TestClass("test1", 13, 3, 9),
                new TestClass("test1", 14, 3, 9),
                new TestClass("test0", 15, 3, 9),
                new TestClass("test1", 16, 3, 9),
                new TestClass("test0", 17, 3, 9),
                new TestClass("test1", 18, 3, 9),
                new TestClass("test1", 19, 3, 9),
                new TestClass("test0", 30, 3, 9),
                new TestClass("test0", 11, 3, 9),
                new TestClass("test1", 12, 3, 9),
                new TestClass("test0", 13, 3, 9),
                new TestClass("test1", 14, 3, 9),
                new TestClass("test1", 15, 3, 9),
                new TestClass("test1", 16, 3, 9),
                new TestClass("test0", 17, 3, 9),
                new TestClass("test1", 18, 3, 9),
                new TestClass("test0", 19, 3, 9),
                new TestClass("test1", 40, 3, 9),
                new TestClass("test1", 11, 3, 9),
                new TestClass("test0", 12, 3, 9),
                new TestClass("test1", 13, 3, 9),
                new TestClass("test0", 14, 3, 9),
                new TestClass("test0", 15, 3, 9),
                new TestClass("test1", 16, 3, 9),
                new TestClass("test0", 17, 3, 9),
                new TestClass("test1", 18, 3, 9),
                new TestClass("test0", 19, 3, 9),
                new TestClass("test1", 50, 3, 9)
            }.AsQueryable();
        }

        [TearDown]
        public void TearDown()
        {
            m_example = null;
            m_exampleQueryable = null;
        }

        private TestClass m_example;
        private IQueryable<TestClass> m_exampleQueryable;

        #endregion

        #region No ignored properties

        [Test]
        public void NoIgnoredPropertiesNoHitTest()
        {
            var filtered = m_exampleQueryable.FilterByExample(new TestClass("asd", 100, 100, 100));

            Assert.That(filtered, Is.Not.Null);
            Assert.That(filtered.Count(), Is.EqualTo(0));
        }

        [Test]
        public void NoIgnoredProperties1HitTest()
        {
            var filtered = m_exampleQueryable.FilterByExample(m_example);

            Assert.That(filtered.Count(), Is.EqualTo(1));
        }

        [Test]
        public void NoIgnoredPropertiesMultipleHitsTest()
        {
            var filtered = m_exampleQueryable.FilterByExample(new TestClass("test0", 9, 3, 9));

            Assert.That(filtered.Count(), Is.EqualTo(5));
        }

        [Test]
        public void NoIgnoredPropertiesMultiTest()
        {
            var filtered0 = m_exampleQueryable.FilterByExample(new TestClass("asd", 100, 100, 100)).AsCachedQuery();
            var filtered1 = m_exampleQueryable.FilterByExample(m_example).AsCachedQuery();
            var filtered5 = m_exampleQueryable.FilterByExample(new TestClass("test0", 9, 3, 9)).AsCachedQuery();

            Assert.That(filtered0.Count(), Is.EqualTo(0));
            Assert.That(filtered1.Count(), Is.EqualTo(1));
            Assert.That(filtered5.Count(), Is.EqualTo(5));
        }

        #endregion

        #region Ignored properties

        [Test]
        public void OneIgnoredPropertiesTest()
        {
            var filtered = m_exampleQueryable.FilterByExample<TestClass>(new TestClass("asd", 9, 3, 9), x => x.TestString);

            Assert.That(filtered.Count(), Is.EqualTo(5));
        }

        [Test]
        public void TwoIgnoredPropertiesTest()
        {
            var filtered0 = m_exampleQueryable.FilterByExample<TestClass>(new TestClass("asd", 50, 100, 100), x => x.TestLong, x => x.TestString);
            var filtered1 = m_exampleQueryable.FilterByExample<TestClass>(new TestClass("test0", 50, 100, 100), x => x.TestLong, x => x.TestInt);

            Assert.That(filtered0.Count(), Is.EqualTo(1));
            Assert.That(filtered1.Count(), Is.EqualTo(30));
        }

        #endregion

        #region Allignored

        [Test]
        public void AllignoredTest()
        {
            var filtered = m_exampleQueryable.FilterByExample<TestClass>(m_example, x => x.TestInt, x => x.TestLong, x => x.TestString);
            Assert.That(filtered.Count(), Is.EqualTo(m_exampleQueryable.Count()));
        }

        //As expected: This will NOT compile (good thing. ;-) )
        //[Test]
        //[ExpectedException(typeof(ArgumentException))]
        //public void OverignoredPlusReadonlyTest()
        //{
        //    var filtered = m_exampleQueryable.FilterByExample<TestClass>(m_example, x => x.TestInt, x => x.TestLong, x => x.TestString, x => x.ReadOnly);
        //}

        #endregion
    }
}

