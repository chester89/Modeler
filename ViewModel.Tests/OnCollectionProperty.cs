using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ViewModeler.Tests
{
    [TestFixture]
    public class OnCollectionProperty
    {
        [Test]
        public void DefaultRuntimeCollectionTypeIsCollectionClosedTypeConfigured()
        {
            var vm = new TestViewModel();
            var a = vm.List;
            var propertyRuntimeType = vm.GetType().GetProperty("List").GetValue(vm, null).GetType();
            Assert.True(propertyRuntimeType.IsClosedTypeOf(typeof(ConcurrentObservableCollection<>)));
        }
    }
}
