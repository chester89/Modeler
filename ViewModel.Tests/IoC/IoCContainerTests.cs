using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ViewModeler.IoC;
using ViewModeler.Validation;

namespace ViewModeler.Tests.IoC
{
    [TestFixture]
    public class IoCContainerTests
    {
        [Test]
        public void DefaultResolverUsesStructureMap()
        {
            Assert.IsInstanceOf<StructureMapDependencyResolver>(IoCContainer.Resolver);
        }

        [Test]
        public void DefaultValidationProviderUsesFluentValidation()
        {
            var validationProvider = IoCContainer.Resolver.TryGetInstance<IValidationProvider>();

            Assert.IsInstanceOf<FluentValidationProvider>(validationProvider);
        }

        [Test]
        public void FluentValidationImplHasSingletonScope()
        {
            var validationProvider = IoCContainer.Resolver.TryGetInstance<IValidationProvider>();
            var validationProvider2 = IoCContainer.Resolver.TryGetInstance<IValidationProvider>();

            Assert.AreSame(validationProvider, validationProvider2);
        }

        [Test]
        public void CanBuildEveryConcreteTypeMapped()
        {
            IoCContainer.Resolver.AssertConfigurationIsValid();
        }

        [Test]
        public void CallToSetResolverSetsResolverProperty()
        {
            IoCContainer.SetResolver(null);

            Assert.AreEqual(IoCContainer.Resolver, null);

            IoCContainer.UseDefaults();
        }

        [Test]
        public void CallToUseDefaultsAppliesStructureMapImplementation()
        {
            IoCContainer.UseDefaults();

            Assert.IsInstanceOf<StructureMapDependencyResolver>(IoCContainer.Resolver);
        }

        [Test]
        public void DefaultMessengerIsMessengerUsingMBshow()
        {
            var messenger = IoCContainer.Resolver.TryGetInstance<IMessenger>();

            Assert.IsInstanceOf<Messenger>(messenger);
        }
    }
}