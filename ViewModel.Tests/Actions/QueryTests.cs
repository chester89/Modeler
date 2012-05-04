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
    public class QueryTests
    {
        private const string loading = "Loading";
        private FormViewModel viewModel;
        private ActionBase command;
        private PropertySubscriber<FormViewModel> subscriber;
        protected string faulted = "Faulted";

        [SetUp]
        public void SetUp()
        {
            viewModel = new FormViewModel();
            subscriber = new PropertySubscriber<FormViewModel>(viewModel);
        }

        [Test]
        public void IfNoExceptionsOccurThenShouldSetViewModelStateToLoadingThenToStill()
        {
            command = new Query(p => { });
            command.SetViewModel(viewModel);
            var events = new List<string>();

            const string still = "BackToStill";
            subscriber.SubscribeTo(vm => vm.State, () =>
                                                       {
                                                           if (viewModel.State == ViewModelState.Loading)
                                                           {
                                                               events.Add(loading);
                                                           }
                                                           if (viewModel.State == ViewModelState.Still)
                                                           {
                                                               events.Add(still);
                                                           }
                                                       });

            Task.Factory.StartNew(() => command.Execute(null)).ContinueWith(prev =>
                                                                                {
                                                                                    Assert.True(events.Count == 2);
                                                                                    Assert.True(events[0] == loading);
                                                                                    Assert.True(events[1] == still);
                                                                                });
        }

        [Test]
        public void IfExceptionsOccuredThenShouldSetViewModelStateToLoadingThenToFaulted()
        {
            command = new Query(p => { throw new EventLogReadingException(); });
            command.SetViewModel(viewModel);

            var events = new List<string>();

            subscriber.SubscribeTo(vm => vm.State, () =>
                                                       {
                                                           if (viewModel.State == ViewModelState.Loading)
                                                           {
                                                               events.Add(loading);
                                                           }
                                                           if (viewModel.State == ViewModelState.Faulted)
                                                           {
                                                               events.Add(faulted);
                                                           }
                                                       });

            Task.Factory.StartNew(() => command.Execute(null)).ContinueWith(prev =>
                                                                                {
                                                                                    Assert.True(events.Count == 2);
                                                                                    Assert.True(events[0] == loading);
                                                                                    Assert.True(events[1] == faulted);
                                                                                });
        }
    }
}