using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ViewModel.Models;

namespace ViewModel
{
    /// <summary>
    /// This collection allows to perform thread-safe operation on <see cref="ObservableCollection{T}"/>
    /// </summary>
    /// <typeparam name="T">The type of elements stored inside collection</typeparam>
    public class ConcurrentObservableCollection<T>: ObservableCollection<T>
    {
        private readonly ICollectionRunner collectionRunner;

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

    public interface ICollectionRunner
    {
        void Run(Action action);
    }

    public class CollectionRunner: ICollectionRunner
    {
        public void Run(Action action)
        {
            ViewModelBase.RunInUi(action);
        }
    }
}
