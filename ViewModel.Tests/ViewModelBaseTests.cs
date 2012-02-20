using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using ViewModel.Models;
using ViewModel.TestUtil;

namespace ViewModel.Tests
{
    [TestFixture]
    public class ViewModelBaseTests
    {
        private Mock<ViewModelBase> viewModelMock;
        private PropertySubscriber<ViewModelBase> subscriber;

        public ViewModelBase ViewModel 
        {
            get { return viewModelMock.Object; }
        }

        [SetUp]
        public void SetUp()
        {
            viewModelMock = new Mock<ViewModelBase>()
                                {
                                    CallBase = true
                                };

            subscriber = new PropertySubscriber<ViewModelBase>(ViewModel);

            //ViewModelBase.SetDispatcher(new MockDispatcher());
        }

        [Test]
        public void ValidationIsOnByDefault()
        {
            Assert.That(ViewModel.IsValidationOn);
        }

        [Test]
        public void StateIsStillByDefault()
        {
            Assert.That(ViewModel.State == ViewModelState.Still);
        }

        [Test]
        public void SettingParentToSelfThrowsException()
        {
            Assert.Throws<ArgumentException>(() => ViewModel.SetParent(ViewModel), "Can't set Parent property to itself");
        }

        [Test]
        public void CallToSetParentSetsParentProperty()
        {
            var parent = new FormViewModel();

            ViewModel.SetParent(parent);

            Assert.That(ViewModel.Parent == parent);
        }

        [Test]
        public void CallToSetStateSetsStateProperty()
        {
            ViewModel.SetState(ViewModelState.Faulted);

            Assert.That(ViewModel.State == ViewModelState.Faulted);
        }

        [Test]
        public void CallToSetStateDoesntChangeStatePropertyIfSameValueProvided()
        {
            ViewModel.SetState(ViewModelState.Uploading);

            subscriber.SubscribeTo(vm => vm.State, () =>
                                                       {
                                                           throw new ArgumentException("Dont do that thing again!");
                                                       });

            Assert.DoesNotThrow(() => ViewModel.SetState(ViewModelState.Uploading));
        }

        [Test]
        public void CallToSelectSetsIsSelectedProperty()
        {
            ViewModel.Select();

            Assert.IsTrue(ViewModel.IsSelected);
        }

        protected virtual void ChangingPropertyLeadsToNPCEventRaised(string propertyName, Action<ViewModelBase> action)
        {
            viewModelMock.Setup(x => x.NotifyPropertyChanged(It.IsAny<string>())).Raises(
                x => x.PropertyChanged += null, new PropertyChangedEventArgs(propertyName));

            action(ViewModel);

            viewModelMock.Verify(vm => vm.NotifyPropertyChanged(propertyName), Times.Once());
        }

        [Test]
        public void ChangingPropertyRaisesCorrespondingEvent()
        {
            ChangingPropertyLeadsToNPCEventRaised("IsSelected", vm => vm.Select());
        }

        [Test]
        public void OnUiThreadThrowsIfDispatcherNotInitialized()
        {
            Assert.Throws<ArgumentException>(() => ViewModel.OnUiThread(() => { }));
        }
    }
}
