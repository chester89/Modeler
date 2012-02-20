using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using StructureMap.Configuration.DSL;

namespace ViewModel.IoC
{
    class StructureMapDependencyConfigurator : IDependencyConfigurator
    {
        public IDependencyResolver Configure()
        {
            var registries = ScanForRegistries();
            return new StructureMapDependencyResolver(registries);
        }

        /// <summary>
        /// Looks for any registry that's exported via MEF
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Registry> ScanForRegistries()
        {
            var directoryCatalog = new DirectoryCatalog(Environment.CurrentDirectory);
            var container = new CompositionContainer(directoryCatalog);
            return container.GetExportedValues<Registry>();
        }
    }
}