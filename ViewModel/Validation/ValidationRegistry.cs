using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                         cfg.AssembliesFromPath(Environment.CurrentDirectory, assembly => 
                             assembly.FullName.Contains("ViewModel.Tests"));
                         if (Assembly.GetEntryAssembly() != null)
                         {
                             cfg.Assembly(Assembly.GetEntryAssembly());
                         }
                         cfg.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                     });
        }
    }
}
