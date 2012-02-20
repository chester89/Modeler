using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;
using NUnit.Framework;
using ViewModel.Validation;

namespace ViewModel.Tests.Validation
{
    [TestFixture]
    public class FluentValidationResultTests
    {
        private IValidationResult validationResult;

        [Test]
        public void ResultIndicatesValidWhenNoValidationErrorsFound()
        {
            var innerResult = new ValidationResult();
            validationResult = new FluentValidationResult(innerResult);

            bool isModelValid = validationResult.IsModelValid();

            Assert.True(isModelValid);
        }

        [Test]
        public void ResultShowsNoErrorsWhenNoneWereFound()
        {
            var innerResult = new ValidationResult();
            validationResult = new FluentValidationResult(innerResult);

            int totalErrorCount = validationResult.GetErrorCount();

            Assert.That(totalErrorCount == 0);
        }

        [Test]
        public void ResultShowErrorsForInvalidProperty()
        {
            var innerResult = new ValidationResult();
            string propertyName = "AnyProperty";
            string errorMessage = "Just some error message, doesnt matter";
            innerResult.Errors.Add(new ValidationFailure(propertyName, errorMessage));
            validationResult = new FluentValidationResult(innerResult);

            var errorsForProperty = validationResult.GetErrorsForProperty(propertyName);

            Assert.That(errorsForProperty.Count() == 1);
            Assert.That(errorsForProperty.First() == errorMessage);
        }

        [Test]
        public void ResultShowNoErrorsForValidProperty()
        {
            var innerResult = new ValidationResult();
            validationResult = new FluentValidationResult(innerResult);

            var errorsForProperty = validationResult.GetErrorsForProperty("NewProperty");

            Assert.That(errorsForProperty.Count() == 0);
        }
    }
}
