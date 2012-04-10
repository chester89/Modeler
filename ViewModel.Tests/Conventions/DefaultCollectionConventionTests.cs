using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Moq;
using NUnit.Framework;
using ViewModel.Conventions;
using ViewModel.Models;

namespace ViewModel.Tests.Conventions
{
    class MockCollectionRunner : ICollectionRunner
    {
        public void Run(Action action)
        {
            action();
        }
    }

    public class DefaultCollectionConventionTests : BaseConventionTest
    {
        private ICollectionRunner collectionRunner;

        #region Setup/Teardown

        private Type CloseCollectionWith<T>()
        {
            return Convention.OpenGenericTypeForCollection.MakeGenericType(new[] {typeof(T)});
        }

        private ICollection<T> GetCollectionInstance<T>()
        {
            return Activator.CreateInstance(CloseCollectionWith<T>(), new [] { collectionRunner }) as ICollection<T>;
        }

        public override void SetUp()
        {
            base.SetUp();
            Convention = new DefaultCollectionConvention();
            collectionRunner = new MockCollectionRunner();

            PropertyMock.SetupProperty(m => m.PropertyType, CloseCollectionWith<double>());
        }

        #endregion

        public override void OnPropertyGetCallsCorrespondingMethodOnParameter()
        {
            PropertyMock.SetupProperty(m => m.PropertyValue, null);

            base.OnPropertyGetCallsCorrespondingMethodOnParameter();
        }

        [Test]
        public void AppliesOnlyToCollections()
        {
            //Act
            bool collectionResult = Convention.Applies<TestViewModel>(vm => vm.List);
            bool commandResult = Convention.Applies<TestViewModel>(vm => vm.Edit);
            bool scalarResult = Convention.Applies<TestViewModel>(vm => vm.Message);

            //Assert
            Assert.True(collectionResult);
            Assert.False(commandResult);
            Assert.False(scalarResult);
        }

        [Test]
        public void CallToPropertyGetCreatesEmptyCollectionIfPropertyIsNull()
        {
            //Arrange
            var closedCollectionType = CloseCollectionWith<string>();
            PropertyMock.SetupProperty(pr => pr.PropertyType, closedCollectionType);
            PropertyMock.SetupProperty(pr => pr.PropertyValue, null);
            //Act
            Convention.OnPropertyGet(Property);
            //Assert
            Assert.IsInstanceOf(closedCollectionType, Property.PropertyValue);
        }

        [Test]
        public void CallToSetParentCallsCorrespondingMethodOnLastInstanceInCollection()
        {
            //Arrange
            ViewModelBase.SetDispatcher(new MockDispatcher());
            var collection = GetCollectionInstance<TestViewModel>();
            collection.Add(new TestViewModel {Id = 20, Message = "This is pure on testing purposes"});
            collection.Add(new TestViewModel {Id = 44});
            var instance = new TestViewModel();
            PropertyMock.Setup(pr => pr.Instance).Returns(instance);
            PropertyMock.SetupProperty(pr => pr.PropertyType, collection.GetType());
            PropertyMock.SetupProperty(pr => pr.PropertyValue, collection);
            //Act
            Convention.SetParent(Property);
            //Assert
            Assert.True(collection.Last().Parent.Equals(instance));
        }

        [Test]
        public void CallToPropertySetCauseSubscriptionToCollectionChangedAndSetParentOnLastElement()
        {
            //Arrange
            ViewModelBase.SetDispatcher(new MockDispatcher());
            var collection = GetCollectionInstance<TestViewModel>();
            collection.Add(new TestViewModel {Id = 20, Message = "This is pure on testing purposes"});
            collection.Add(new TestViewModel { Id = 44 });
            int eventRaised = -1;

            const int someTestStubValue = 100;
            (collection as INotifyCollectionChanged).CollectionChanged += (e, a) =>
                                                {
                                                    eventRaised = someTestStubValue;
                                                };
            var instance = new TestViewModel();
            PropertyMock.Setup(pr => pr.Instance).Returns(instance);
            PropertyMock.SetupProperty(pr => pr.PropertyType, collection.GetType());
            PropertyMock.SetupProperty(pr => pr.PropertyValue, collection);
            
            //Act
            Convention.OnPropertySet(Property);
            collection.Add(new TestViewModel());
            //Assert
            Assert.That(eventRaised == someTestStubValue);
            Assert.True(collection.Last().Parent.Equals(instance));
        }
    }
}