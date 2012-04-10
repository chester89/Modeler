using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Input;
using StructureMap.Configuration.DSL;
using ViewModel.Actions;
using ViewModel.Conventions;

namespace ViewModel.IoC.Registries
{
    [Export(typeof(Registry))]
    public class CommonRegistry: Registry
    {
        public CommonRegistry()
        {
            For(typeof (ICollection<>)).Use(typeof (ConcurrentObservableCollection<>));
            Scan(c =>
                     {
                         c.ExcludeType<Command>();
                         c.TheCallingAssembly();
                         c.WithDefaultConventions();
                     });
        }
    }
}
