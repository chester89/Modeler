using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{

    public interface ICollectionBuilder
    {
        /// <summary>
        /// Returns minimum collection interface used on ViewModel properties. The return type should be generic
        /// </summary>
        Type GetMinimumCollectionInterface();
        /// <summary>
        /// Returns open generic type used for collection properties in ViewModel classes
        /// </summary>
        /// <returns></returns>
        Type GetOpenGenericCollectionType();
    }

    public class CollectionBuilder: ICollectionBuilder
    {
        public Type GetMinimumCollectionInterface()
        {
            return typeof(ICollection<>);
        }

        public Type GetOpenGenericCollectionType()
        {
            return typeof(ConcurrentObservableCollection<>);
        }
    }
}
