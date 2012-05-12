using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using StructureMap.Configuration.DSL;
using StructureMap.Configuration.DSL.Expressions;
using StructureMap.Graph;
using ViewModeler.Actions;
using ViewModeler.Conventions;
using ViewModeler.Infrastructure;

namespace ViewModeler.IoC.Registries
{
    public class CommonRegistry: Registry
    {
        public CommonRegistry()
        {
            Scan(c =>
                     {
                         c.LookForRegistries();
                         c.ExcludeType<Command>();
                         c.TheCallingAssembly();
                         c.AddAllTypesOf<IPropertyConvention>();
                         c.AddAllTypesOf<IExpressionHandler>();
                         c.WithDefaultConventions();
                         c.Convention<GenericCollectionConvention>();
                     });
        }
    }

    /// <summary>
    /// Scans for implementation of <see cref="ICollection{T}"/> interface
    /// </summary>
    class GenericCollectionConvention: IRegistrationConvention
    {
        private Type openGenericInterfaceCollectionType = typeof (ICollection<>);

        public void Process(Type type, Registry registry)
        {
            if (type.IsClosedTypeOf(openGenericInterfaceCollectionType))
            {
                registry.AddType(openGenericInterfaceCollectionType, type);
            }
        }
    }
}
