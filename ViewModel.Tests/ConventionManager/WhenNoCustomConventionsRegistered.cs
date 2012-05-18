using NUnit.Framework;
using ViewModeler.Conventions;
using CM = ViewModeler.Conventions.ConventionManager;

namespace ViewModeler.Tests.ConventionManager
{
    [TestFixture]
    public class WhenNoCustomConventionsRegistered
    {
        private CM manager;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            manager = new CM();
        }

        [Test]
        public void UseOnlyDefaultConventionsPerforms()
        {
            manager.UseOnlyDefaultConventions();

            Assert.False(manager.IsEmpty, "Convention manager should be empty but it's not");
        }

        //Add more tests to make sure manager clears it's state after registering custom conventions and then calling UseOnlyDefaultConventions()

        [Test]
        public void RetrievesDefaultConventionForScalarProperty()
        {
            //Arrange
            var viewModel = new TestViewModel();
            //Act
            var convention = manager.Convention(viewModel, "Value");
            //Assert
            Assert.IsNotNull(convention);
            Assert.IsInstanceOf<DefaultScalarConvention>(convention);
        }

        [Test]
        public void RetrievesDefaultConventionForCollectionProperty()
        {
            //Arrange
            var viewModel = new TestViewModel();
            //Act
            var convention = manager.Convention(viewModel, "List");
            //Assert
            Assert.IsNotNull(convention);
            Assert.IsInstanceOf<DefaultCollectionConvention>(convention);
        }

        [Test]
        public void RetrievesDefaultConventionForCommand()
        {
            //Arrange
            var vm = new TestViewModel();
            //Act
            var convention = manager.Convention(vm, "Edit");
            //Assert
            Assert.IsNotNull(convention);
            Assert.IsInstanceOf<DefaultCommandConvention>(convention);
        }
    }
}
