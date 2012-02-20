using System.Reflection;
using System.Windows.Input;

namespace ViewModel.Conventions
{
    public class DefaultCommandConvention: ConventionBase
    {
        public override void OnPropertyGet(IPropertyInfo info)
        {
            info.ProceedGet();
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
