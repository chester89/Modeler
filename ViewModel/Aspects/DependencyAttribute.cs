using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostSharp.Aspects;
using PostSharp.Extensibility;
using PostSharp.Reflection;
using ViewModel.IoC;

namespace ViewModel.Aspects
{
    [Serializable]
    public class DependencyAttribute: LocationInterceptionAspect
    {
        public override bool CompileTimeValidate(LocationInfo locationInfo)
        {
            var resolver = IoCContainer.Resolver;
            var type = locationInfo.LocationType;

            if (!type.IsInterface)
            {
                Message.Write(new LocationInfo(locationInfo.FieldInfo), SeverityType.Error, "001",
                                "Dependency can only be used on interfaces in {0}.{1}",
                                locationInfo.DeclaringType, locationInfo.Name);
                return false;
            }

            if (!resolver.HasImplementationsFor(type))
            {
                Message.Write(new LocationInfo(locationInfo.FieldInfo), SeverityType.Error, "002",
                        "A concrete type was not found for {0}.{1}",
                        locationInfo.DeclaringType, locationInfo.Name);
                return false;
            }
            return true;
        }

        public override void OnGetValue(LocationInterceptionArgs args)
        {
            args.ProceedGetValue();
            if (args.Value == null)
            {
                args.Value = IoCContainer.Resolver.TryGetInstance(args.Location.LocationType);
            }
            args.ProceedGetValue();
        }
    }
}
