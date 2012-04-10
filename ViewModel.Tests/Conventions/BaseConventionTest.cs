using Moq;
using NUnit.Framework;
using ViewModel.Conventions;

namespace ViewModel.Tests.Conventions
{
    [TestFixture]
    public abstract class BaseConventionTest
    {
        protected Mock<IPropertyInfo> PropertyMock;
        protected ConventionBase Convention;
        private string FailureMessage = "IPropertyInfo.Proceed{0} wasn't called while executing impl of " + typeof(IPropertyConvention).FullName + ".OnProperty{0}";

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
            Assert.DoesNotThrow(() => PropertyMock.Verify(m => m.ProceedSet(), Times.Once(), string.Format(FailureMessage, "Get")));
        }

        [Test]
        public virtual void OnPropertyGetCallsCorrespondingMethodOnParameter()
        {
            //Arrange
            PropertyMock.Setup(x => x.ProceedGet()).Verifiable();

            //Act
            Convention.OnPropertyGet(Property);

            //Assert
            Assert.DoesNotThrow(() => PropertyMock.Verify(m => m.ProceedGet(), Times.Once(), string.Format(FailureMessage, "Set")));
        }
    }
}