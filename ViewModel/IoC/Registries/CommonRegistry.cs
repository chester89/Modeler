using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap.Configuration.DSL;
using ViewModeler.Actions;
using ViewModeler.Conventions;

namespace ViewModeler.IoC.Registries
{
    public class CommonRegistry: Registry
    {
        public CommonRegistry()
        {
            For(typeof (ICollection<>)).Use(typeof (ConcurrentObservableCollection<>));
            Scan(c =>
                     {
                         c.LookForRegistries();
                         c.ExcludeType<Command>();
                         c.TheCallingAssembly();
                         c.AddAllTypesOf<IPropertyConvention>();
                         c.WithDefaultConventions();
                     });
        }
    }
}
