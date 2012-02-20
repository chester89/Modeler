using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ViewModel.MappingConventions;

namespace ViewModel.Tests.PropertyMapping
{
    [TestFixture]
    public class StandardDateTimeMappingConventionTests
    {
        private PropertyMappingConventionBase<DateTime> convention;

        [SetUp]
        public void SetUp()
        {
            convention = new StandardDateTimeConvention();
        }

        [Test]
        public void MappingForDateTimeWorks()
        {
            var arbitraryDate = new DateTime(2010, 10, 15, 10, 0, 0);
            var mappingResult = convention.Map(arbitraryDate);

            Assert.That(mappingResult, Is.EqualTo(DateTime.SpecifyKind(arbitraryDate, DateTimeKind.Utc).ToLocalTime()));
        }

        [Test]
        public void MappingForNullableAnalogWorks()
        {
            var arbitraryDate = new DateTime(2010, 10, 15, 10, 0, 0);
            var mappingResult = convention.Map(new DateTime?(arbitraryDate));

            Assert.That(mappingResult, Is.EqualTo(DateTime.SpecifyKind(arbitraryDate, DateTimeKind.Utc).ToLocalTime()));
        }
    }
}
