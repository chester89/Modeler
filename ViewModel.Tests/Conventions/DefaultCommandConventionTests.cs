using System;
using Moq;
using NUnit.Framework;
using ViewModel.Actions;
using ViewModel.Conventions;
using ViewModel.Models;

namespace ViewModel.Tests.Conventions
{
    public class DefaultCommandConventionTests : BaseConventionTest
    {
        public override void SetUp()
        {
            base.SetUp();

            Convention = new DefaultCommandConvention(collectionBuilderMock.Object);
        }

        [Test]
        public void AppliesOnlyToICommand()
        {
            var result = Convention.Applies<TestViewModel>(vm => vm.Edit);
            var differentResult = Convention.Applies<TestViewModel>(vm => vm.Message);

            Assert.True(result);
            Assert.False(differentResult);
        }

        [Test]
        public void SetParentCallDoesntSetParentProperty()
        {
            //Act
            Convention.SetParent(Property);

            //Assert
            ViewModelBase parentAfterCall = Property.Instance.Parent;
            Assert.IsNull(parentAfterCall);
        }

        [Test]
        public void OnPropertySet_AssignesViewModel()
        {
            Action<Object> emptyExecute = p => {};
            Predicate<Object> emptyCanExecute = t => true;
            var commandMock = new Mock<Command>(new object[] { emptyExecute, emptyCanExecute });
            var propertyMock = new Mock<IPropertyInfo>();
            var viewModelMock = new Mock<ViewModelBase>() { CallBase = true };

            commandMock.Setup(m => m.SetViewModel(viewModelMock.Object)).Verifiable("SetViewModel wasn't called!");
            propertyMock.Setup(p => p.PropertyValue).Returns(commandMock.Object).Verifiable("hey!");
            propertyMock.Setup(p => p.Instance).Returns(viewModelMock.Object).Verifiable("hey!!!");
                
            Convention.OnPropertyGet(propertyMock.Object);
            commandMock.Verify(c => c.SetViewModel(viewModelMock.Object), Times.Once());
        }

        [Test]
        public void OnPropertySetDoesntInvokePropertyChanged()
        {
            //Arrange
            const string propValue = "SimpleProp";
            PropertyMock.Setup(m => m.PropertyName).Returns(propValue);
            
            PropertyMock.Setup(m => m.Instance.NotifyPropertyChanged(It.IsAny<string>())).Throws<InvalidOperationException>();

            //Act
            Convention.OnPropertySet(Property);

            //Assert
            const string failureMessage = "Notification about property changed shouldn't happen but it did";
            Assert.DoesNotThrow(() => PropertyMock.Verify(m => m.Instance.NotifyPropertyChanged(Property.PropertyName), Times.Never(), failureMessage));
        }
    }
}
