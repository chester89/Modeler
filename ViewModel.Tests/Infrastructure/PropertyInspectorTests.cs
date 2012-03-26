using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NUnit.Framework;
using ViewModel.Infrastructure;

namespace ViewModel.Tests.Infrastructure
{
    [Ignore("Not ready yet")]
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
        public void ThrowsIfCantHandleExpression()
        {
            Expression<Func<Stream, long>> expression = stream => stream.Seek(20, SeekOrigin.Begin);

            Assert.Throws<ArgumentException>(() => inspector.PathFor(expression));
        }
    }
}
