using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.Models;

namespace ViewModel
{
    /// <summary>
    /// Provides access to instantiation point of every collection property on <see cref="ViewModelBase"/> subclasses
    /// </summary>
    public interface ICollectionBuilder
    {
        /// <summary>
        /// Returns minimum collection interface used on ViewModel properties. The return type should be generic
        /// </summary>
        Type GetMinimumCollectionInterface();
        /// <summary>
        /// Returns open generic type used for collection properties in <see cref="ViewModelBase"/> subclasses
        /// </summary>
        /// <returns></returns>
        Type GetOpenGenericCollectionType();
    }

    /// <summary>
    /// Default implementation of <see cref="ICollectionBuilder"/> interface
    /// </summary>
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
