using System;
using System.ComponentModel;
using System.Linq;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Extensibility;
using ViewModeler.IoC;
using ViewModeler.Models;

namespace ViewModeler.Aspects
{
    [Serializable]
    [IntroduceInterface(typeof(IDataErrorInfo), OverrideAction = InterfaceOverrideAction.Ignore)]
    [MulticastAttributeUsage(MulticastTargets.Class, Inheritance = MulticastInheritance.Strict)]
    public sealed class DataErrorInfoAspect: InstanceLevelAspect, IDataErrorInfo
    {
        [IntroduceMember(OverrideAction = MemberOverrideAction.Ignore)]
        public string Error
        {
            get { return string.Empty; }
        }

        [IntroduceMember(OverrideAction = MemberOverrideAction.Ignore)]
        public string this[string columnName]
        {
            get { return GetValidationErrorsForProperty(columnName); }
        }

        //[MulticastPointcut(Targets = MulticastTargets.Property, Attributes = MulticastAttributes.Instance | MulticastAttributes.NonAbstract)]
        string GetValidationErrorsForProperty(string propertyName)
        {
            IValidationProvider validationProvider = IoCContainer.Resolver.TryGetInstance<IValidationProvider>();

            var viewModel = Instance as ViewModelBase;
            if(viewModel.IsValidationOn)
            {
                var errors = validationProvider.Validate(viewModel).GetErrorsForProperty(propertyName).ToArray();
                if (errors.Any())
                {
                    return string.Join(", ", errors);
                }
            }
            return string.Empty;
        }
    }
}
