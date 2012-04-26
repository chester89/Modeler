using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using ViewModel.MappingConventions;
using ViewModel.Models;

namespace ViewModel.Conventions
{
    public class DefaultScalarConvention: ConventionBase
    {
        private readonly PropertyMappingManager mappingManager;

        public DefaultScalarConvention(ICollectionBuilder collectionBuilder): base(collectionBuilder)
        {
            mappingManager = new PropertyMappingManager();
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

            MapProperty(info);
        }

        void MapProperty(IPropertyInfo info)
        {
            var methodToInvoke = mappingManager.GetType().GetMethod("PropertyMappingFor").MakeGenericMethod(new[] { info.PropertyType });

            dynamic mappingConvention = methodToInvoke.Invoke(mappingManager, new object[] { });
            if (mappingConvention != null)
            {
                info.PropertyValue = mappingConvention.Map(info.PropertyValue);
                info.ProceedSet();
            }
        }

        protected override bool AppliesCore(PropertyInfo property)
        {
            return (!property.PropertyType.IsClosedTypeOf(collectionBuilder.GetMinimumCollectionInterface()) &&
                    !property.PropertyType.IsAssignableFrom(typeof(ICommand)));
        }
    }
}
