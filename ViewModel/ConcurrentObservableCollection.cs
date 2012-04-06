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
        protected override void InsertItem(int index, T item)
        {
            ViewModelBase.RunInUi(() => base.InsertItem(index, item));
        }

        protected override void ClearItems()
        {
            ViewModelBase.RunInUi(() => base.ClearItems());
        }

        protected override void RemoveItem(int index)
        {
            ViewModelBase.RunInUi(() => base.RemoveItem(index));
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            ViewModelBase.RunInUi(() => base.MoveItem(oldIndex, newIndex));
        }

        protected override void SetItem(int index, T item)
        {
            ViewModelBase.RunInUi(() => base.SetItem(index, item));
        }
    }
}
