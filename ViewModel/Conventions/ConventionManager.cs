using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Models;

namespace ViewModel.Conventions
{
    public class ConventionManager
    {
        List<IPropertyConvention> conventions;

        public ConventionManager()
        {
            conventions = new List<IPropertyConvention>();

            //by default, use default conventions only
            UseOnlyDefaultConventions();
        }

        public void UseOnlyDefaultConventions()
        {
            conventions.Clear();

            conventions.Add(new DefaultScalarConvention());
            conventions.Add(new DefaultCollectionConvention());
            conventions.Add(new DefaultCommandConvention());
        }

        public Boolean IsEmpty 
        { 
            get { return !conventions.Any(); }
        }

        /// <summary>
        /// Searches for a convention that applies on <paramref name="instance"/> object and <paramref name="propertyName"/>
        /// </summary>
        public IPropertyConvention Convention(ViewModelBase instance, String propertyName)
        {
            var customConvention = conventions.SingleOrDefault(c => c.Applies(instance.GetType(), propertyName));
            if (customConvention != null)
            {
                return customConvention;
            }
            throw new ArgumentException("Convention for requested type was not found. Probable reasons are that you haven't it registered (in case you use custom implementation) or it's Applies() method not functioning properly");
        }
    }
}
