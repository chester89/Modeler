using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NUnit.Framework;
using ViewModel.Infrastructure;

namespace ViewModel.Tests.Infrastructure
{
    [TestFixture]
    public class PropertyHandlerTests
    {
        private PropertyHandler handler;

        [SetUp]
        public void SetUp()
        {
            handler = new PropertyHandler();
        }

        [Test]
        public void CanHandleLambdaExpressions()
        {
            //Arrange
            Expression<Func<TestViewModel, string>> expression = t => t.Message;

            //Act
            bool result = handler.CanHandle(expression);

            //Assert
            Assert.True(result);
        }

        [Test]
        public void BuildPathFromPropertyAccess()
        {
            //Arrange
            Expression<Func<TestViewModel, string>> expression = t => t.Message;

            //Act
            string result = handler.Handle(expression, string.Empty, ".");

            //Assert
            Assert.That(result, Is.EqualTo(".Message"));
        }
    }
}
