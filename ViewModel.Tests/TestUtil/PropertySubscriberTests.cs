using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ViewModel.TestUtil;

namespace ViewModel.Tests.TestUtil
{
    [TestFixture]
    public class PropertySubscriberTests
    {
        [Test]
        public void SubscribesToViewModelProperty()
        {
            var vm = new TestViewModel();
            var subscriber = SubscriberExtensions.CreateSubscriber(vm);

            subscriber.SubscribeTo(x => x.Message, () =>
                                                       {
                                                           throw new InvalidCastException();
                                                       });
            Assert.Throws<InvalidCastException>(() => vm.Message = "Hello man!");
        }
    }
}
