using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ViewModel.Actions;
using ViewModel.Models;

namespace ViewModel.Tests.Actions
{
    public class ValidationCommandTests: CommandTests
    {
        private ActionBase command;
        private FormViewModel formViewModel = new FormViewModel();

        [SetUp]
        public void SetUp()
        {
            command = new ValidationCommand(null, null, formViewModel);
        }

        [Test]
        public void GivenModelValidationFails_WhenCommandFires_ThenCanExecuteReturnsFalse()
        {
            Assert.IsFalse(command.CanExecute(null));
        }

        [Test]
        public void GivenModelBecomeValid_WhenCommandFires_ThenCanExecuteReturnsTrue()
        {
            Assert.IsFalse(command.CanExecute(null));

            formViewModel.Text = "hello";
            formViewModel.Title = "My name is  ";

            Assert.IsTrue(command.CanExecute(null));

            formViewModel = new FormViewModel();
        }
    }
}
