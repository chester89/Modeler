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

            convention = new DefaultCommandConvention();
        }

        [Test]
        public void AppliesOnlyToICommand()
        {
            var result = convention.Applies<TestViewModel>(vm => vm.Edit);
            var differentResult = convention.Applies<TestViewModel>(vm => vm.Message);

            Assert.True(result);
            Assert.False(differentResult);
        }

        [Test]
        public void SetParentCallDoesntSetParentProperty()
        {
            //Act
            convention.SetParent(Property);

            //Assert
            ViewModelBase parentAfterCall = Property.Instance.Parent;
            Assert.IsNull(parentAfterCall);
        }

        [Test]
        public void OnPropertySetDoesntInvokePropertyChanged()
        {
            //Arrange
            const string propValue = "SimpleProp";
            propertyMock.Setup(m => m.PropertyName).Returns(propValue);
            
            propertyMock.Setup(m => m.Instance.NotifyPropertyChanged(It.IsAny<string>())).Throws<InvalidOperationException>();

            //Act
            convention.OnPropertySet(Property);

            //Assert
            const string failureMessage = "Notification about property changed shouldn't happen but it did";
            Assert.DoesNotThrow(() =>
                                    {
                                        propertyMock.Verify(m => m.Instance.NotifyPropertyChanged(Property.PropertyName), Times.Never(), failureMessage);
                                    });
        }
    }
}
