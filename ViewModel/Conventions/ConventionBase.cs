using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ViewModel.Infrastructure;

namespace ViewModel.Conventions
{
    public abstract class ConventionBase : IPropertyConvention
    {
        private string path;
        private static readonly PropertyInspector Inspector;

        static ConventionBase()
        {
            Inspector = new PropertyInspector();
        }

        public ConventionBase(): this(".")
        {
            
        }

        public ConventionBase(string path = ".")
        {
            this.path = path;
        }

        /// <summary>
        /// Specifies open generic type for collection properties
        /// </summary>
        public Type OpenGenericTypeForCollection
        {
            get { return typeof (ObservableCollection<>); }
        }

        public bool Applies(Type targetType, string propertyName)
        {
            var property = targetType.GetProperty(propertyName);

            if (property == null)
            {
                throw new ArgumentException(string.Format("Property {0} wasn't found on type {1}", propertyName,
                                                          targetType.FullName));
            }
            return AppliesCore(property);
        }

        protected abstract bool AppliesCore(PropertyInfo property);
        public abstract void OnPropertyGet(IPropertyInfo info);
        public abstract void OnPropertySet(IPropertyInfo info);
        public abstract void SetParent(IPropertyInfo info);
        
        public bool Applies<T>(Expression<Func<T, Object>> expression)
        {
            var propertyName = Inspector.PathFor(expression);
            return Applies(typeof (T), propertyName);
        }
    }
}
