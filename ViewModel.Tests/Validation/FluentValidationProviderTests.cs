using NUnit.Framework;
using ViewModeler.Models;
using ViewModeler.Validation;

namespace ViewModeler.Tests.Validation
{
    [TestFixture]
    public class FluentValidationProviderTests
    {
        private IValidationProvider provider;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            provider = new FluentValidationProvider();
        }

        [Test]
        public void VmIsValidIfNoValidatorsDefined()
        {
            var result = provider.Validate(new TestViewModel());

            Assert.That(result.IsModelValid());
        }

        [Test]
        public void InvalidViewModelRaisesErrors()
        {
            var viewModel = new FormViewModel()
                                {
                                    Text = "2", 
                                    Title = "This cant be true"
                                };

            var result = provider.Validate(viewModel);

            Assert.That(!result.IsModelValid(), "Model should be invalid but it's not");
            Assert.That(result.GetErrorCount() == 2, "There should be 2 errors");
        }
    }
}
