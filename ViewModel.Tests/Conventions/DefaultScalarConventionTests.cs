using System;
using Moq;
using NUnit.Framework;
using ViewModel.Conventions;
using ViewModel.Models;

namespace ViewModel.Tests.Conventions
{
    public class DefaultScalarConventionTests : BaseConventionTest
    {
        #region Setup/Teardown

        public override void SetUp()
        {
            base.SetUp();

            convention = new DefaultScalarConvention();
        }

        #endregion

        public override void OnPropertySetCallsCorrespondingMethodOnParameter()
        {
            const int something = 20;
            const int somethingElse = 55;
            propertyMock.SetupProperty(m => m.PropertyValue, somethingElse);
            propertyMock.SetupProperty(m => m.CurrentValue, something);

            base.OnPropertySetCallsCorrespondingMethodOnParameter();
        }

        [Test]
        public void OnPropertySetCallsSetParentOnParameter()
        {
            //Arrange
            var testViewModel = new TestViewModel();
            propertyMock.SetupProperty(x => x.PropertyValue, testViewModel);
            propertyMock.SetupProperty(x => x.PropertyType, testViewModel.GetType());

            //Act
            convention.OnPropertySet(Property);

            //Assert
            Assert.That(testViewModel.Parent.Equals(Property.Instance));
        }

        [Test]
        public void OnPropertySetNotifiesAboutPropertyChanged()
        {
            //Arrange
            Action action = () =>
                                {
                                    
                                };
            propertyMock.Setup(m => m.Instance.NotifyPropertyChanged(It.IsAny<string>())).Callback(action);

            Property.Instance.NotifyPropertyChanged("Hello");

            propertyMock.Verify(m => m.Instance.NotifyPropertyChanged(It.IsAny<string>()), Times.AtLeastOnce());
        }

        [Test]
        public void AppliesOnlyToScalarProperties()
        {
            //Act
            var scalarResult = convention.Applies<TestViewModel>(vm => vm.Message);
            var collectionResult  = convention.Applies<TestViewModel>(vm => vm.List);
            var commandResult = convention.Applies<TestViewModel>(vm => vm.Edit);
            //Assert
            Assert.True(scalarResult);
            Assert.False(collectionResult);
            Assert.False(commandResult);
        }
    }
}