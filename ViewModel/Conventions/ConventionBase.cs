using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ViewModel.Infrastructure;

namespace ViewModel.Conventions
{
    /// <summary>
    /// Provides basic functionality for expanding property workflow
    /// </summary>
    public abstract class ConventionBase : IPropertyConvention
    {
        protected readonly ICollectionBuilder collectionBuilder;
        private string path;
        private static readonly PropertyInspector Inspector;

        static ConventionBase()
        {
            Inspector = new PropertyInspector();
        }

        public ConventionBase(ICollectionBuilder collectionBuilder): this(".")
        {
            this.collectionBuilder = collectionBuilder;
        }

        private ConventionBase(string path = ".")
        {
            this.path = path;
        }

        public bool Applies(Type targetType, string propertyName)
        {
            var property = targetType.GetProperty(propertyName);

            if (property == null)
            {
                throw new ArgumentException(string.Format("Property {0} wasn't found on type {1}", propertyName, targetType.FullName));
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
