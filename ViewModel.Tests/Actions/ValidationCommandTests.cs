using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ViewModel.Tests.Actions
{
    public class ValidationCommandTests: CommandTests
    {
        private FormViewModel formViewModel = new FormViewModel();

        [Test]
        public void GivenModelValidationFails_WhenCommandFires_ThenCanExecuteReturnsFalse()
        {
            Assert.IsFalse(formViewModel.Edit.CanExecute(null));
        }

        [Test]
        public void GivenModelBecomeValid_WhenCommandFires_ThenCanExecuteReturnsTrue()
        {
            Assert.IsFalse(formViewModel.Edit.CanExecute(null));

            formViewModel.Text = "hello";
            formViewModel.Title = "My name is  ";

            Assert.IsTrue(formViewModel.Edit.CanExecute(null));

            formViewModel = new FormViewModel();
        }
    }
}
