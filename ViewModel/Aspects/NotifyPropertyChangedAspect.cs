using System;
using System.Linq;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Extensibility;
using ViewModel.Conventions;
using ViewModel.MappingConventions;
using ViewModel.Models;

namespace ViewModel.Aspects
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Class, Inheritance = MulticastInheritance.Strict)]
    public sealed class NotifyPropertyChangedAspect : InstanceLevelAspect
    {
        [NonSerialized]
        private ConventionManager conventionManager;

        public override void RuntimeInitialize(Type type)
        {
            conventionManager = new ConventionManager();
        }

        [OnLocationGetValueAdvice(Master="OnPropertySet"), MulticastPointcut(Targets = MulticastTargets.Property, Attributes = MulticastAttributes.Instance | MulticastAttributes.NonAbstract | MulticastAttributes.Public)]
        public void OnPropertyGet(LocationInterceptionArgs args)
        {
            var vmInstance = args.Instance as ViewModelBase;
            var convention = conventionManager.Convention(vmInstance, args.Location.Name);

            convention.OnPropertyGet(new PostSharpPropertyInfo(args));
        }

        [OnLocationSetValueAdvice, MulticastPointcut(Targets = MulticastTargets.Property, Attributes = MulticastAttributes.Instance | MulticastAttributes.NonAbstract | MulticastAttributes.Public)]
        public void OnPropertySet(LocationInterceptionArgs args)
        {
            var vmInstance = args.Instance as ViewModelBase;
            var convention = conventionManager.Convention(vmInstance, args.Location.Name);

            convention.OnPropertySet(new PostSharpPropertyInfo(args));
        }

        //private void InvokeOnPropertySet(LocationInterceptionArgs args, string propertyName)
        //{
        //    var beforePropertySet = Instance.GetType().GetMethod("On" + propertyName + "Set", BindingFlags.NonPublic | BindingFlags.Instance);

        //    if (beforePropertySet != null)
        //    {
        //        object[] invocationParameters = null;

        //        if (beforePropertySet.GetParameters().Count() > 0)
        //        {
        //            invocationParameters = new[] { args.Value };
        //        }
        //        try
        //        {
        //            beforePropertySet.Invoke(Instance, invocationParameters);
        //        }
        //        catch (TargetInvocationException exception)
        //        {
        //            throw exception.InnerException ?? exception;
        //        }
        //    }
        //}
    }
}
