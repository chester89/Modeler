using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// Determines whether <paramref name="closedType"/> is closed type of <paramref name="genericType"/>, like IEnumerable of int is closed type of IEnumerable of T
        /// </summary>
        public static bool IsClosedTypeOf(this Type closedType, Type genericType)
        {
            if (genericType.IsGenericTypeDefinition)
            {
                var arguments = closedType.GetGenericArguments();
                if (arguments.Any())
                {
                    var closedGenericType = genericType.MakeGenericType(arguments);
                    if (closedGenericType == closedType || closedGenericType.IsAssignableTo(genericType))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsAssignableTo<T>(this Type sourceType)
        {
            return typeof (T).IsAssignableFrom(sourceType);
        }

        public static bool IsAssignableTo(this Type sourceType, Type leftHandSideType)
        {
            return leftHandSideType.IsAssignableFrom(sourceType);
        }
    }
}
