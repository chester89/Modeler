using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using FluentValidation;
using StructureMap.Configuration.DSL;

namespace ViewModel.Validation
{
    public class ValidationRegistry : Registry
    {
        public ValidationRegistry()
        {
            Scan(cfg =>
                     {
                         cfg.TheCallingAssembly();
                         cfg.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                     });
        }
    }
}
