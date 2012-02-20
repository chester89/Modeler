using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using ViewModel.Conventions;

namespace ViewModel.Tests.Conventions
{
    [TestFixture]
    public class ConventionBaseTests
    {
        private Mock<ConventionBase> conventionMock;

        public ConventionBase Convention
        {
            get { return conventionMock.Object; }
        }

        [SetUp]
        public void SetUp()
        {
            conventionMock = new Mock<ConventionBase>() { CallBase = true };
        }

        [Test]
        public void ThrowsExceptionWhenPropertyNotFound()
        {
            Assert.Throws<ArgumentException>(() => Convention.Applies(typeof (Stream), "fdkgjdngkd"));
        }
    }
}
