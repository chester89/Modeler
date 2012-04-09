using System;
using Moq;
using NUnit.Framework;
using ViewModel.Conventions;
using ViewModel.Models;

namespace ViewModel.Tests.Conventions
{
    public class DefaultCommandConventionTests : BaseConventionTest
    {
        public override void SetUp()
        {
            base.SetUp();

            Convention = new DefaultCommandConvention();
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
            Assert.DoesNotThrow(() =>
                                    {
                                        PropertyMock.Verify(m => m.Instance.NotifyPropertyChanged(Property.PropertyName), Times.Never(), failureMessage);
                                    });
        }
    }
}
