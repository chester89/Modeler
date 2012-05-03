using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    public static class CommonExtensions
    {
        public static IEnumerable<T> ToEnumerableOf<T>(this object propertyValue)
        {
            var collection = propertyValue as IEnumerable;
            if (collection == null)
            {
                throw new ArgumentException("Object provided is not an enumerable of any kind", "propertyValue");
            }
            return collection.OfType<T>();
        }
    }
}
