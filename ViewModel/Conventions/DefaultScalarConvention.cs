using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using ViewModel.Models;

namespace ViewModel.Conventions
{
    public class DefaultScalarConvention: ConventionBase
    {
        public DefaultScalarConvention(ICollectionBuilder collectionBuilder): base(collectionBuilder)
        {
        }

        public override void SetParent(IPropertyInfo info)
        {
            var viewModel = info.PropertyValue as ViewModelBase;
            if (viewModel != null)
            {
                viewModel.SetParent(info.Instance);
            }
        }

        public override void OnPropertyGet(IPropertyInfo info)
        {
            info.ProceedGet();
        }

        public override void OnPropertySet(IPropertyInfo info)
        {
            // Don't go further if the new value is equal to the old one.
            if (info.PropertyValue.Equals(info.CurrentValue))
                return;

            info.ProceedSet();
            info.Instance.NotifyPropertyChanged(info.PropertyName);
            SetParent(info);
        }

        protected override bool AppliesCore(PropertyInfo property)
        {
            return (!property.PropertyType.IsClosedTypeOf(collectionBuilder.GetMinimumCollectionInterface()) &&
                    !property.PropertyType.IsAssignableFrom(typeof(ICommand)));
        }
    }
}
