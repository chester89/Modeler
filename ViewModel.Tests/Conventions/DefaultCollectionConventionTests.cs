using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using ViewModel.Conventions;

namespace ViewModel.Tests.Conventions
{
    public class DefaultCollectionConventionTests : BaseConventionTest
    {
        #region Setup/Teardown

        public override void SetUp()
        {
            base.SetUp();
            propertyMock.SetupProperty(m => m.PropertyType, typeof (ObservableCollection<double>));

            convention = new DefaultCollectionConvention();
        }

        #endregion

        public override void OnPropertyGetCallsCorrespondingMethodOnParameter()
        {
            propertyMock.SetupProperty(m => m.PropertyValue, null);

            base.OnPropertyGetCallsCorrespondingMethodOnParameter();
        }

        [Test]
        public void AppliesOnlyToCollections()
        {
            //Act
            bool collectionResult = convention.Applies<TestViewModel>(vm => vm.List);
            bool commandResult = convention.Applies<TestViewModel>(vm => vm.Edit);
            bool scalarResult = convention.Applies<TestViewModel>(vm => vm.Message);

            //Assert
            Assert.True(collectionResult);
            Assert.False(commandResult);
            Assert.False(scalarResult);
        }

        [Test]
        public void CallToPropertyGetCreatesEmptyCollectionIfPropertyIsNull()
        {
            //Arrange
            propertyMock.SetupProperty(pr => pr.PropertyType, typeof (ObservableCollection<string>));
            propertyMock.SetupProperty(pr => pr.PropertyValue, null);
            //Act
            convention.OnPropertyGet(Property);
            //Assert
            Assert.IsInstanceOf<ObservableCollection<string>>(Property.PropertyValue);
        }

        [Test]
        public void CallToPropertyGetCreatesEmptyCollectionWith2GenericParametersIfPropertyIsNull()
        {
            //Arrange
            propertyMock.SetupProperty(pr => pr.PropertyType, typeof (Dictionary<double, DateTime>));
            propertyMock.SetupProperty(pr => pr.PropertyValue, null);
            //Act
            convention.OnPropertyGet(Property);
            //Assert
            Assert.IsInstanceOf<Dictionary<double, DateTime>>(Property.PropertyValue);
        }

        [Test]
        public void CallToSetParentCallsCorrespondingMethodOnLastInstanceInCollection()
        {
            //Arrange
            var collection = new ObservableCollection<TestViewModel>
                                 {
                                     new TestViewModel {Id = 20, Message = "This is pure on testing purposes"},
                                     new TestViewModel {Id = 44}
                                 };
            var instance = new TestViewModel();
            propertyMock.Setup(pr => pr.Instance).Returns(instance);
            propertyMock.SetupProperty(pr => pr.PropertyType, collection.GetType());
            propertyMock.SetupProperty(pr => pr.PropertyValue, collection);
            //Act
            convention.SetParent(Property);
            //Assert
            Assert.True(collection.Last().Parent.Equals(instance));
        }

        [Test]
        public void CallToPropertySetCauseSubscriptionToCollectionChangedAndSetParentOnLastElement()
        {
            //Arrange
            var collection = new ObservableCollection<TestViewModel>
                                 {
                                     new TestViewModel {Id = 20, Message = "This is pure on testing purposes"},
                                     new TestViewModel {Id = 44}
                                 };
            int eventRaised = -1;

            collection.CollectionChanged += (e, a) =>
                                                {
                                                    eventRaised = 100;
                                                };
            var instance = new TestViewModel();
            propertyMock.Setup(pr => pr.Instance).Returns(instance);
            propertyMock.SetupProperty(pr => pr.PropertyType, collection.GetType());
            propertyMock.SetupProperty(pr => pr.PropertyValue, collection);
            
            //Act
            convention.OnPropertySet(Property);
            collection.Add(new TestViewModel());
            //Assert
            Assert.That(eventRaised == 100);
            Assert.True(collection.Last().Parent.Equals(instance));
        }
    }
}