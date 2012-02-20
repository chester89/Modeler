using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ViewModel.Actions;
using ViewModel.Models;

namespace ViewModel.Tests.Actions
{
    public class ValidationQueryTests: QueryTests
    {
        private ActionBase command;
        private FormViewModel formViewModel;

        [SetUp]
        public void SetUp()
        {
            formViewModel = new FormViewModel();
            command = new ValidationQuery(null, null, formViewModel);
        }

        [Test]
        public void GivenModelValidationFails_WhenQueryFires_ThenCanExecuteReturnsFalse()
        {
            Assert.IsFalse(command.CanExecute(null));
        }

        [Test]
        public void GivenModelBecomeValid_WhenQueryFires_ThenCanExecuteReturnsTrue()
        {
            Assert.IsFalse(command.CanExecute(null));

            formViewModel.Text = "hello";
            formViewModel.Title = "My name is  ";

            Assert.IsTrue(command.CanExecute(null));

            formViewModel = new FormViewModel();
        }
    }
}
