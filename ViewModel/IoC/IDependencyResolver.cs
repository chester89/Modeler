using System;
using System.Collections.Generic;

namespace ViewModeler.IoC
{
    /// <summary>
    /// Provides a contract for using your IoC framework of choice
    /// </summary>
    public interface IDependencyResolver
    {
        T GetInstance<T>();
        Object GetInstance(Type requestedType);
        T TryGetInstance<T>();
        Object TryGetInstance(Type requestedType);
        IList<T> GetAllInstances<T>();
        //bool HasImplementationsFor<T>();
        //bool HasImplementationsFor(Type requestedType);
        void AssertConfigurationIsValid();
    }
}