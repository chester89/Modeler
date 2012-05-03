using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ViewModel.Models;

namespace ViewModel
{
    /// <summary>
    /// This collection allows to perform operations <see cref="ObservableCollection{T}"/>
    /// </summary>
    /// <typeparam name="T">The type of elements stored inside collection</typeparam>
    public class ConcurrentObservableCollection<T>: ObservableCollection<T>
    {
        private readonly ICollectionRunner collectionRunner;

        /// <summary>
        /// Initializes new instance of <see cref="ConcurrentObservableCollection{T}"/> class
        /// </summary>
        /// <param name="collectionRunner"><see cref="ICollectionRunner"/> implementation</param>
        public ConcurrentObservableCollection(ICollectionRunner collectionRunner)
        {
            this.collectionRunner = collectionRunner;
        }

        protected override void InsertItem(int index, T item)
        {
            collectionRunner.Run(() => base.InsertItem(index, item));
        }

        protected override void ClearItems()
        {
            collectionRunner.Run(() => base.ClearItems());
        }

        protected override void RemoveItem(int index)
        {
            collectionRunner.Run(() => base.RemoveItem(index));
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            collectionRunner.Run(() => base.MoveItem(oldIndex, newIndex));
        }

        protected override void SetItem(int index, T item)
        {
            collectionRunner.Run(() => base.SetItem(index, item));
        }
    }

    /// <summary>
    /// Wraps any operation on <see cref="ConcurrentObservableCollection{T}"/>
    /// </summary>
    public interface ICollectionRunner
    {
        void Run(Action action);
    }

    /// <summary>
    /// Default implementation of <see cref="ICollectionRunner"/> interface
    /// </summary>
    public class CollectionRunner: ICollectionRunner
    {
        public void Run(Action action)
        {
            ViewModelBase.RunInUi(action);
        }
    }
}
