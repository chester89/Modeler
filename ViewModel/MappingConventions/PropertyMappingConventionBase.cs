using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.MappingConventions
{
    public abstract class PropertyMappingConventionBase<T>
    {
        protected Converter<T, T> mapper;
 
        protected PropertyMappingConventionBase()
        {
            mapper = input => input;
        }

        public T Map(Object propertyValue)
        {
            if (propertyValue == null)
            {
                throw new ArgumentNullException("propertyValue");
            }
            return mapper(HandleMapping(propertyValue));
        }

        protected virtual T HandleMapping(Object propertyValue)
        {
            return (T) propertyValue;
        }
    }
}
