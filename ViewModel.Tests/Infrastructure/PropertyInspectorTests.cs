using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NUnit.Framework;
using ViewModeler.Infrastructure;

namespace ViewModeler.Tests.Infrastructure
{
    [TestFixture]
    public class PropertyInspectorTests
    {
        private PropertyInspector inspector;

        [SetUp]
        public void SetUp()
        {
            inspector = new PropertyInspector();
        }

        [Test]
        public void PathIsEmptyIfCantHandleExpression()
        {
            Expression<Func<Stream, long>> expression = stream => stream.Seek(20, SeekOrigin.Begin);
            Assert.AreEqual(string.Empty, inspector.PathFor(expression));
        }

        [Test]
        public void CanBuildPathForComplexProperties()
        {
            Expression<Func<TestViewModel, int>> expression = t => t.SomeDate.Month;
            string result = inspector.PathFor(expression);
            Assert.AreEqual("SomeDate.Month", result);
        }
    }
}
