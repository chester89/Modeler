using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ViewModeler.Validation;

namespace ViewModeler.Tests.Validation
{
    [TestFixture]
    public class DataAnnotationsValidationResultTests
    {
        private DataAnnotationValidationResult validationResult;

        [SetUp]
        public void SetUp()
        {
            validationResult = new DataAnnotationValidationResult();
        }

        [Test]
        public void ResultShowsNoErrorsOnNonexistingProperty()
        {
            var errors = validationResult.GetErrorsForProperty("Name");

            Assert.That(!errors.Any());
        }

        [Test]
        public void ResultShowsErrorsOnInvalidProperty()
        {
            string propertyName = "Text";
            string errorMessage = "Should be longer that 10 symbols";
            validationResult.AddError(propertyName, errorMessage);

            var errors = validationResult.GetErrorsForProperty(propertyName);

            Assert.That(errors.Count() == 1);
            Assert.That(errors.First() == errorMessage);
        }

        [Test]
        public void ResultIndicatesValidWhenNoValidationErrorsFound()
        {
            bool isModelValid = validationResult.IsModelValid();

            Assert.True(isModelValid);
        }

        [Test]
        public void AddErrorCallAddsErrorForCurrentlyValidProperty()
        {
            string propertyName = "Text";
            string errorMessage = "Should be longer than 10 symbols";

            Assert.That(!validationResult.GetErrorsForProperty(propertyName).Any());
            
            validationResult.AddError(propertyName, errorMessage);

            var errors = validationResult.GetErrorsForProperty(propertyName);

            Assert.That(errors.Count() == 1);
            Assert.That(errors.First() == errorMessage);
        }

        [Test]
        public void AddErrorCallAddsErrorForCurrentlyInvalidProperty()
        {
            string propertyName = "Text";
            string errorMessage = "Should be shorter than 150 symbols";

            Assert.That(!validationResult.GetErrorsForProperty(propertyName).Any());

            validationResult.AddError(propertyName, errorMessage);

            var errors = validationResult.GetErrorsForProperty(propertyName);

            Assert.That(errors.Count() == 1);
            Assert.That(errors.First() == errorMessage);

            string newErrorMessage = "Should not contain Russian letters";
            validationResult.AddError(propertyName, newErrorMessage);

            errors = validationResult.GetErrorsForProperty(propertyName);

            Assert.That(errors.Count() == 2);
            Assert.That(errors.ToArray()[1] == newErrorMessage);
        }
    }
}
