using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.MappingConventions
{
    public class PropertyMappingManager
    {
        private Dictionary<Type, Object> mappings; 

        public PropertyMappingManager()
        {
            mappings = new Dictionary<Type, object> {{typeof (DateTime), new StandardDateTimeConvention()}};
            //what to do here? scan app dll + app dlls via MEF?    
        }

        public PropertyMappingConventionBase<T> PropertyMappingFor<T>()
        {
            if (mappings.ContainsKey(typeof(T)))
            {
                return mappings[typeof (T)] as PropertyMappingConventionBase<T>;
            }
            return null;
        }
    }
}
