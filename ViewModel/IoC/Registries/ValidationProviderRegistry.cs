using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap.Configuration.DSL;
using ViewModeler.Validation;

namespace ViewModeler.IoC.Registries
{
    public class ValidationProviderRegistry: Registry
    {
        public ValidationProviderRegistry()
        {
            ForSingletonOf<IValidationProvider>().Use<FluentValidationProvider>();
        }
    }
}
