using NUnit.Framework;
using ViewModeler.Models;
using ViewModeler.Validation;
using System.ComponentModel.DataAnnotations;

namespace ViewModeler.Tests.Validation
{
    class DAViewModel: ViewModelBase
    {
        [StringLength(20, MinimumLength = 15)]
        public string Message { get; set; }

        [System.ComponentModel.DataAnnotations.Range(50, 100)]
        public int SomeValue { get; set; }
    }

    [TestFixture]
    public class DataAnnotationValidationProviderTests
    {
        private IValidationProvider validationProvider;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            validationProvider = new DataAnnotationsValidationProvider();
        }

        [Test]
        public void VmIsValidWhenNoValidatorsInPlace()
        {
            var vm = new TestViewModel();

            var validationResult = validationProvider.Validate(vm);

            Assert.True(validationResult.IsModelValid());
            Assert.True(validationResult.GetErrorCount() == 0);
        }

        [Test]
        public void InvalidVmRaisesErrors()
        {
            var vm = new DAViewModel()
                         {
                             Message = "Here I am", 
                             SomeValue = 10
                         };

            var validationResult = validationProvider.Validate(vm);

            Assert.False(validationResult.IsModelValid());
            Assert.True(validationResult.GetErrorCount() == 2);
        }
    }
}
