using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ViewModel.Tests.Actions
{
    [Description("Tests CanExecute capabilities")]
    [TestFixture]
    public class CanExecuteTests
    {
        private TestViewModel viewModel;

        [SetUp]
        public void SetUp()
        {
            viewModel = new TestViewModel()
                            {
                                Id = -10
                            };
        }

        [Test]
        public void CanExecuteIsCorrectlyCalculatedFromPropertiesItDependsOn()
        {
            Assert.False(viewModel.ForProperties.CanExecute(null));
        }

        [Test]
        public void ChangingPropertyValuesChangesCanExecute()
        {
            viewModel.Id = 20;
            Assert.True(viewModel.ForProperties.CanExecute(null));
        }

        [Test]
        public void ChangingPropertyValuesReevaluatesCanExecute()
        {
            viewModel.Id = 100;
            Assert.True(viewModel.ForProperties.CanExecute(null));
            viewModel.Id = -30;
            Assert.False(viewModel.ForProperties.CanExecute(null));
        }
    }
}
