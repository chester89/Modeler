using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap.Configuration.DSL;
using ViewModel.Actions;
using ViewModel.Conventions;

namespace ViewModel.IoC.Registries
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
