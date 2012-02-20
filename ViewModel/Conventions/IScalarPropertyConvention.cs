using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ViewModel.Models;

namespace ViewModel.Conventions
{
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

    public interface IPropertyConvention
    {
        bool Applies(Type targetType, string propertyName);
        bool Applies<T>(Expression<Func<T, Object>>  expression);
        void OnPropertyGet(IPropertyInfo info);
        void OnPropertySet(IPropertyInfo info);
        void SetParent(IPropertyInfo info);
    }
}
