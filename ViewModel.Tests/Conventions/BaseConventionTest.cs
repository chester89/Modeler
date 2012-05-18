using System.Collections.Generic;
using System.Collections.ObjectModel;
using Moq;
using NUnit.Framework;
using ViewModeler.Actions;
using ViewModeler.Conventions;

namespace ViewModeler.Tests.Conventions
{
    [TestFixture]
    public abstract class BaseConventionTest
    {
        protected Mock<IPropertyInfo> PropertyMock;
        protected ConventionBase Convention;
        private string FailureMessage = "IPropertyInfo.Proceed{0} wasn't called while executing implementation of " + typeof(IPropertyConvention).FullName + ".OnProperty{0}";
        protected Mock<ICollectionBuilder> collectionBuilderMock;

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
            collectionBuilderMock = new Mock<ICollectionBuilder>();
            collectionBuilderMock.Setup(c => c.GetMinimumCollectionInterface()).Returns(typeof (ICollection<>));
            collectionBuilderMock.Setup(c => c.GetOpenGenericCollectionType()).Returns(typeof (ObservableCollection<>));
        }

        [Test]
        public virtual void OnPropertySetCallsCorrespondingMethodOnParameter()
        {
            //Arrange
            PropertyMock.Setup(m => m.ProceedSet()).Verifiable();

            //Act
            Convention.OnPropertySet(Property);

            //Assert
            Assert.DoesNotThrow(() => PropertyMock.Verify(m => m.ProceedSet(), Times.Once(), string.Format(FailureMessage, "Set")));
        }

        [Test]
        public virtual void OnPropertyGetCallsCorrespondingMethodOnParameter()
        {
            //Arrange
            PropertyMock.Setup(x => x.ProceedGet()).Verifiable();
            PropertyMock.Setup(x => x.PropertyValue).Returns(new Command(p => { }));

            //Act
            Convention.OnPropertyGet(Property);

            //Assert
            Assert.DoesNotThrow(() => PropertyMock.Verify(m => m.ProceedGet(), Times.Once(), string.Format(FailureMessage, "Get")));
        }
    }
}