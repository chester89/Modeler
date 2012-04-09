using Moq;
using NUnit.Framework;
using ViewModel.Conventions;
using ViewModel.Tests;

namespace ViewModel.Tests.Conventions
{
    [TestFixture]
    public abstract class BaseConventionTest
    {
        protected Mock<IPropertyInfo> PropertyMock;
        protected ConventionBase Convention;

        public IPropertyInfo Property
        {
            get { return PropertyMock.Object; }
        }

        [SetUp]
        public virtual void SetUp()
        {
            PropertyMock = new Mock<IPropertyInfo>();

            var testViewModel = new TestViewModel();
            PropertyMock.Setup(m => m.Instance).Returns(testViewModel);
        }

        [Test]
        public virtual void OnPropertySetCallsCorrespondingMethodOnParameter()
        {
            //Arrange
            PropertyMock.Setup(m => m.ProceedSet()).Verifiable();

            //Act
            Convention.OnPropertySet(Property);

            //Assert
            var failureMessage =
                string.Format("IPropertyInfo.ProceedSet() wasn't called while executing impl of {0}.OnPropertySet",
                              typeof (IPropertyConvention).FullName);
            Assert.DoesNotThrow(() => PropertyMock.Verify(m => m.ProceedSet(), Times.Once(), failureMessage));
        }

        [Test]
        public virtual void OnPropertyGetCallsCorrespondingMethodOnParameter()
        {
            //Arrange
            PropertyMock.Setup(x => x.ProceedGet()).Verifiable();

            //Act
            Convention.OnPropertyGet(Property);

            //Assert
            var failureMessage =
                string.Format("IPropertyInfo.ProceedGet() wasn't called while executing impl of {0}.OnPropertyGet",
                              typeof(IPropertyConvention).FullName);
            Assert.DoesNotThrow(() => PropertyMock.Verify(m => m.ProceedGet(), Times.Once(), failureMessage));
        }
    }
}