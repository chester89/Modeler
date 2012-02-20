using Moq;
using NUnit.Framework;
using ViewModel.Conventions;
using ViewModel.Tests;

namespace ViewModel.Tests.Conventions
{
    [TestFixture]
    public abstract class BaseConventionTest
    {
        protected Mock<IPropertyInfo> propertyMock;
        protected IPropertyConvention convention;

        public IPropertyInfo Property
        {
            get { return propertyMock.Object; }
        }

        [SetUp]
        public virtual void SetUp()
        {
            propertyMock = new Mock<IPropertyInfo>();

            var testViewModel = new TestViewModel();
            propertyMock.Setup(m => m.Instance).Returns(testViewModel);
        }

        [Test]
        public virtual void OnPropertySetCallsCorrespondingMethodOnParameter()
        {
            //Arrange
            propertyMock.Setup(m => m.ProceedSet()).Verifiable();

            //Act
            convention.OnPropertySet(Property);

            //Assert
            var failureMessage =
                string.Format("IPropertyInfo.ProceedSet() wasn't called while executing impl of {0}.OnPropertySet",
                              typeof (IPropertyConvention).FullName);
            Assert.DoesNotThrow(() => propertyMock.Verify(m => m.ProceedSet(), Times.Once(), failureMessage));
        }

        [Test]
        public virtual void OnPropertyGetCallsCorrespondingMethodOnParameter()
        {
            //Arrange
            propertyMock.Setup(x => x.ProceedGet()).Verifiable();

            //Act
            convention.OnPropertyGet(Property);

            //Assert
            var failureMessage =
                string.Format("IPropertyInfo.ProceedGet() wasn't called while executing impl of {0}.OnPropertyGet",
                              typeof(IPropertyConvention).FullName);
            Assert.DoesNotThrow(() => propertyMock.Verify(m => m.ProceedGet(), Times.Once(), failureMessage));
        }
    }
}