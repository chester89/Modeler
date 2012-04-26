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

            Convention = new DefaultScalarConvention(collectionBuilderMock.Object);
        }

        #endregion

        public override void OnPropertySetCallsCorrespondingMethodOnParameter()
        {
            const int something = 20;
            const int somethingElse = 55;
            PropertyMock.SetupProperty(m => m.PropertyValue, somethingElse);
            PropertyMock.SetupProperty(m => m.CurrentValue, something);

            base.OnPropertySetCallsCorrespondingMethodOnParameter();
        }

        [Test]
        public void OnPropertySetCallsSetParentOnParameter()
        {
            //Arrange
            var testViewModel = new TestViewModel();
            PropertyMock.SetupProperty(x => x.PropertyValue, testViewModel);
            PropertyMock.SetupProperty(x => x.PropertyType, testViewModel.GetType());

            //Act
            Convention.OnPropertySet(Property);

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
            PropertyMock.Setup(m => m.Instance.NotifyPropertyChanged(It.IsAny<string>())).Callback(action);

            Property.Instance.NotifyPropertyChanged("Hello");

            PropertyMock.Verify(m => m.Instance.NotifyPropertyChanged(It.IsAny<string>()), Times.AtLeastOnce());
        }

        [Test]
        public void AppliesOnlyToScalarProperties()
        {
            //Act
            var scalarResult = Convention.Applies<TestViewModel>(vm => vm.Message);
            var collectionResult  = Convention.Applies<TestViewModel>(vm => vm.List);
            var commandResult = Convention.Applies<TestViewModel>(vm => vm.Edit);
            //Assert
            Assert.True(scalarResult);
            Assert.False(collectionResult);
            Assert.False(commandResult);
        }
    }
}