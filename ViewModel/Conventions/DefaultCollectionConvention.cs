using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using ViewModel.IoC;
using ViewModel.Models;

namespace ViewModel.Conventions
{
    public class DefaultCollectionConvention : ConventionBase
    {
        public DefaultCollectionConvention(ICollectionBuilder collectionBuilder) : base(collectionBuilder)
        {
        }

        private void OnCollectionChanged(IPropertyInfo info, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                SetParent(info);
            }

            info.Instance.NotifyPropertyChanged(info.PropertyName);
        }

        public override void OnPropertyGet(IPropertyInfo info)
        {
            AssignEmptyCollectionIfPropertyIsNull(info);

            info.ProceedGet();
        }

        private void AssignEmptyCollectionIfPropertyIsNull(IPropertyInfo info)
        {
            Type[] genericArguments = info.PropertyType.GetGenericArguments();

            if (genericArguments.Any() && info.PropertyValue == null)
            {
                info.PropertyValue = IoCContainer.Resolver.TryGetInstance(collectionBuilder.GetMinimumCollectionInterface().MakeGenericType(genericArguments));
            }
        }

        public override void OnPropertySet(IPropertyInfo info)
        {
            info.ProceedSet();
            if (IsCollectionOfDerivedTypeOfViewModel(info.PropertyType))
            {
                var viewModelCollection = info.PropertyValue as INotifyCollectionChanged;
                viewModelCollection.CollectionChanged += (e, a) => OnCollectionChanged(info, a);
            }

            info.Instance.NotifyPropertyChanged(info.PropertyName);
        }

        private bool IsCollectionOfDerivedTypeOfViewModel(Type propertyType)
        {
            Type firstGenericArgument = propertyType.GetGenericArguments().First();
            return propertyType.IsClosedTypeOf(collectionBuilder.GetOpenGenericCollectionType()) && firstGenericArgument.IsAssignableTo<ViewModelBase>();
        }

        private IEnumerable<T> ToEnumerableOf<T>(object propertyValue)
        {
            var collection = propertyValue as ICollection;
            return collection.OfType<T>();
        }

        public override void SetParent(IPropertyInfo info)
        {
            if (IsCollectionOfDerivedTypeOfViewModel(info.PropertyType))
            {
                IEnumerable<ViewModelBase> collection = ToEnumerableOf<ViewModelBase>(info.PropertyValue);
                collection.Last().SetParent(info.Instance);
            }
        }

        protected override bool AppliesCore(PropertyInfo property)
        {
            return property.PropertyType.IsClosedTypeOf(collectionBuilder.GetMinimumCollectionInterface());
        }
    }
}