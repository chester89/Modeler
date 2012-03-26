using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using StructureMap.Configuration.DSL;

namespace ViewModel.IoC.Registries
{
    [Export(typeof(Registry))]
    public class CommonRegistry: Registry
    {
        public CommonRegistry()
        {
            For<IMessenger>().Use<Messenger>();
            Scan(c => c.TheCallingAssembly());
        }
    }
}
