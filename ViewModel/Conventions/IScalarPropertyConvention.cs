using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PostSharp.Aspects;
using ViewModeler.Models;

namespace ViewModeler.Conventions
{
    /// <summary>
    /// Provides access to property data at runtime - it's really just a convenience wrapper around <see cref="LocationInterceptionArgs"/>
    /// </summary>
    public interface IPropertyInfo
    {
        ViewModelBase Instance { get; }
        Object PropertyValue { get; set; }
        Object CurrentValue { get; }
        Type PropertyType { get; set; }
        string PropertyName { get; }
        void ProceedGet();
        void ProceedSet();
    }

    /// <summary>
    /// Provides access to property extension points
    /// </summary>
    public interface IPropertyConvention
    {
        bool Applies(Type targetType, string propertyName);
        bool Applies<T>(Expression<Func<T, Object>>  expression);
        void OnPropertyGet(IPropertyInfo info);
        void OnPropertySet(IPropertyInfo info);
        void SetParent(IPropertyInfo info);
    }
}
