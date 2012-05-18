using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap.Configuration.DSL;
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
            //For(typeof (ICollection<>)).Use(typeof (ConcurrentObservableCollection<>));
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

    class GenericCollectionConvention : IRegistrationConvention
    {
        private Type openGenericInterfaceCollectionType = typeof(ICollection<>);

        public void Process(Type type, Registry registry)
        {
            if (type.IsClosedTypeOf(openGenericInterfaceCollectionType))
            {
                registry.AddType(openGenericInterfaceCollectionType, type);
            }
        }
    }
}
