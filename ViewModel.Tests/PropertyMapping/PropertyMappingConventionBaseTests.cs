using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using ViewModel.MappingConventions;

namespace ViewModel.Tests.PropertyMapping
{
    [TestFixture]
    public class PropertyMappingConventionBaseTests
    {
        private Mock<PropertyMappingConventionBase<int>> mockedConvention;

        public PropertyMappingConventionBase<int> Convention
        {
            get { return mockedConvention.Object; }
        } 

        [SetUp]
        public void SetUp()
        {
            mockedConvention = new Mock<PropertyMappingConventionBase<int>>()
                                   {
                                       CallBase = true
                                   };
        }

        [Test]
        public void ThrowsIfNullablePropertyIsNull()
        {
            int? property = null;

            Assert.Throws<ArgumentNullException>(() => Convention.Map(property));
        }

        [Test]
        public void ThrowsIfPropertyValueIsNullButNotANullableOfSomeType()
        {
            object property = null;

            Assert.Throws<ArgumentNullException>(() => Convention.Map(property));
        }

        [Test]
        public void DoesNothingIfNullablePropertyHasValue()
        {
            int? propertyValue = 34;

            var mappingResult = Convention.Map(propertyValue);

            Assert.AreEqual(propertyValue.Value, mappingResult);
        }
    }
}
