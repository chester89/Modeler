using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentValidation;
using StructureMap.Configuration.DSL;

namespace ViewModeler.Validation
{
    public class ValidationRegistry : Registry
    {
        public ValidationRegistry()
        {
            Scan(cfg =>
                     {
                         var assemblyName = GetType().Assembly.GetName().Name;
                         cfg.AssembliesFromPath(Environment.CurrentDirectory, assembly => assembly.FullName.Contains(string.Format("{0}.Tests", assemblyName)));
                         if (Assembly.GetEntryAssembly() != null)
                         {
                             cfg.Assembly(Assembly.GetEntryAssembly());
                         }
                         cfg.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                     });
        }
    }
}
