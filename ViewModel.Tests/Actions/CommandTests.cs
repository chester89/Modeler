using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NUnit.Framework;
using ViewModel.Actions;
using ViewModel.Models;
using ViewModel.TestUtil;

namespace ViewModel.Tests.Actions
{
    [TestFixture]
    public class CommandTests
    {
        private TestViewModel viewModel;
        private ICommand command;
        private PropertySubscriber<TestViewModel> subscriber;

        [SetUp]
        public void SetUp()
        {
            viewModel = new TestViewModel();
            subscriber = new PropertySubscriber<TestViewModel>(viewModel);

            command = new Command(p => {}, viewModelInstance: viewModel);
        }

        [Test]
        public void IfNoExceptionsOccurThenShouldSetViewModelStateToUploadingThenToStill()
        {
            var events = new List<string>();

            subscriber.SubscribeTo(vm => vm.State, () =>
                                                       {
                                                           if (viewModel.State == ViewModelState.Uploading)
                                                           {
                                                                events.Add("Uploading");
                                                           }
                                                           if (viewModel.State == ViewModelState.Still)
                                                           {
                                                               events.Add("BackToStill");
                                                           }
                                                       });

            Task.Factory.StartNew(() => command.Execute(null)).ContinueWith(prev =>
                                                                                {
                                                                                    Assert.True(events.Count == 2);
                                                                                    Assert.True(events[0] == "Uploading");
                                                                                    Assert.True(events[1] == "BackToStill");
                                                                                });
        }

        [Test]
        public void IfExceptionsOccuredThenShouldSetViewModelStateToUploadingThenToFaulted()
        {
            command = new Command(p =>
                                      {
                                          throw new EventLogReadingException();
                                      }, viewModelInstance: viewModel);

            var events = new List<string>();

            subscriber.SubscribeTo(vm => vm.State, () =>
            {
                if (viewModel.State == ViewModelState.Uploading)
                {
                    events.Add("Uploading");
                }
                if (viewModel.State == ViewModelState.Faulted)
                {
                    events.Add("Faulted");
                }
            });

            Task.Factory.StartNew(() => command.Execute(null)).ContinueWith(prev =>
            {
                Assert.True(events.Count == 2);
                Assert.True(events[0] == "Uploading");
                Assert.True(events[1] == "Faulted");
            });
        }
    }
}
