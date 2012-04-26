using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace ViewModel.IoC
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        readonly IContainer container = new Container();
        public StructureMapDependencyResolver(IEnumerable<Registry> registries)
        {
            foreach (var registry in registries)
            {
                container.Configure(cfg => cfg.AddRegistry(registry));
            }
        }

        public T GetInstance<T>()
        {
            return container.GetInstance<T>();
        }

        public Object GetInstance(Type requestedType)
        {
            return container.GetInstance(requestedType);
        }

        public T TryGetInstance<T>()
        {
            return container.TryGetInstance<T>();
        }

        public Object TryGetInstance(Type requestedType)
        {
            return container.TryGetInstance(requestedType);
        }

        public IList<T> GetAllInstances<T>()
        {
            return container.GetAllInstances<T>();
        }

        public bool HasImplementationsFor<T>()
        {
            return container.Model.HasImplementationsFor<T>();
        }

        public bool HasImplementationsFor(Type requestedType)
        {
            return container.Model.HasImplementationsFor(requestedType);
        }

        public void AssertConfigurationIsValid()
        {
            container.AssertConfigurationIsValid();
        }
    }
}
