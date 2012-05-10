using System.Reflection;
using System.Windows.Input;
using ViewModeler.Actions;

namespace ViewModeler.Conventions
{
    public class DefaultCommandConvention: ConventionBase
    {
        public DefaultCommandConvention(ICollectionBuilder collectionBuilder) : base(collectionBuilder)
        {
        }

        public override void OnPropertyGet(IPropertyInfo info)
        {
            info.ProceedGet();
            var command = info.PropertyValue as ActionBase;
            command.SetViewModel(info.Instance);
        }

        public override void OnPropertySet(IPropertyInfo info)
        {
            info.ProceedSet();
        }

        public override void SetParent(IPropertyInfo info)
        {
            
        }
        
        protected override bool AppliesCore(PropertyInfo property)
        {
            return property.PropertyType.IsAssignableFrom(typeof(ICommand));
        }
    }
}
