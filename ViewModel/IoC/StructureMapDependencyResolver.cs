using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using StructureMap;
using ViewModeler.IoC.Registries;

namespace ViewModeler.IoC
{
    /// <summary>
    /// Implementation of <see cref="IDependencyResolver"/> interface using StructureMap
    /// </summary>
    class StructureMapDependencyResolver : IDependencyResolver
    {
        readonly IContainer container;
        public StructureMapDependencyResolver()
        {
            container = new Container(new CommonRegistry());
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

        //public bool HasImplementationsFor<T>()
        //{
        //    return container.Model.HasImplementationsFor<T>();
        //}

        //public bool HasImplementationsFor(Type requestedType)
        //{
        //    return container.Model.HasImplementationsFor(requestedType);
        //}

        public void AssertConfigurationIsValid()
        {
            container.AssertConfigurationIsValid();
        }
    }
}
