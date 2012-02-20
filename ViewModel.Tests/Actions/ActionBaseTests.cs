using System;
using Moq;
using NUnit.Framework;
using ViewModel.Actions;
using ViewModel.Models;
using ViewModel.TestUtil;

namespace ViewModel.Tests.Actions
{
    [TestFixture]
    public class ActionBaseTests
    {
        private Mock<ActionBase> commandMock;

        public ActionBase Action
        {
            get { return commandMock.Object; }
        }

        [SetUp]
        public void SetUp()
        {
            commandMock = new Mock<ActionBase>(null, null)
                              {
                                  CallBase = true
                              };
            ViewModelBase.SetDispatcher(new MockDispatcher());
        }

        [TearDown]
        public void TearDown()
        {
            ViewModelBase.SetDispatcher(null);
        }

        [Test]
        public void SetViewModelCallThrowsIfParameterIsNull()
        {
            Assert.Throws<ArgumentException>(() => Action.SetViewModel(null),
                                             "viewModel parameter should not be null");
        }

        [Test]
        public void SetViewModelCallsNotifyCanExecuteChanged()
        {
            //Arrange
            var action = new Action(() => { });

            var viewModelMock = new Mock<ViewModelBase>()
                                    {
                                        CallBase = true
                                    };

            viewModelMock.Setup(vm => vm.OnUiThread(action)).Verifiable();
            commandMock.Setup(x => x.NotifyCanExecuteChanged()).Verifiable();

            //Act
            Action.SetViewModel(viewModelMock.Object);

            //Assert
            Assert.DoesNotThrow(() =>
                                    {
                                        viewModelMock.Verify(vm => vm.OnUiThread(action), Times.AtMostOnce());
                                        commandMock.Verify(m => m.NotifyCanExecuteChanged(), Times.AtMostOnce());
                                    });
        }

        [Test]
        public void NotifyCanExecuteChangedRaisesCorrespondingEvent()
        {
            bool raised = false;

            Action.SetViewModel(new TestViewModel());

            Action.CanExecuteChanged += (sender, args) =>
                                            {
                                                raised = true;
                                            };

            Action.NotifyCanExecuteChanged();

            Assert.True(raised);
        }
    }
}
