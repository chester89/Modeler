namespace ViewModel.IoC
{
    /// <summary>
    /// Provides access to Inversion of Control
    /// </summary>
    public class IoCContainer
    {
        public static IDependencyResolver Resolver { get; internal set; }

        private IoCContainer()
        {
        }

        static IoCContainer()
        {
            SetResolverToDefault();
        }

        private static void SetResolverToDefault()
        {
            Resolver = new StructureMapDependencyConfigurator().Configure();
        }

        public static void SetResolver(IDependencyResolver resolver)
        {
            Resolver = resolver;
        }

        public static void UseDefaults()
        {
            SetResolverToDefault();
        }
    }
}
