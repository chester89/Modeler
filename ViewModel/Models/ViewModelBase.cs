using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;
using System.Windows.Threading;
using ViewModel.Aspects;
using ViewModel.Actions;
using ViewModel.Infrastructure;
using ViewModel.IoC;

namespace ViewModel.Models
{
    /// <summary>
    /// Base class for a view model
    /// </summary>
    [NotifyPropertyChangedAspect]
    [DataErrorInfoAspect]
    public abstract class ViewModelBase: INotifyPropertyChanged
    {
        public Boolean IsValidationOn { get; protected set; }

        public ViewModelBase Parent { get; protected set; }
        public ViewModelState State { get; protected set; }
        public bool IsSelected { get; protected set; }
        protected static IDispatcher Dispatcher;

        public static void SetDispatcher(IDispatcher appDispatcher)
        {
            Dispatcher = appDispatcher;
        }

        protected ViewModelBase()
        {
            IsValidationOn = true;
            State = ViewModelState.Still;
        }

        public void SetParent(ViewModelBase parent)
        {
            OnParentSet(parent);
            Parent = parent;
        }

        public void SetState(ViewModelState newState)
        {
            if (State != newState)
            {
                State = newState;
            }
        }

        public virtual void Select()
        {
            IsSelected = true;
        }

        protected virtual void OnParentSet(ViewModelBase newParentValue)
        {
            if (newParentValue.Equals(this))
            {
                throw new ArgumentException("Can't set Parent property to itself");
            }
        }

        protected ICommand DecorateCommand(ActionBase action)
        {
            action.SetViewModel(this);
            return action;
        }

        /// <summary>
        /// Executes action on UI thread
        /// </summary>
        public virtual void OnUiThread(Action action)
        {
            if (Dispatcher != null)
            {
                Dispatcher.Invoke(action);
            }
            else
            {
                throw new ArgumentException("Dispatcher field not initialised!");
            }
        }

        public virtual event PropertyChangedEventHandler PropertyChanged;

        public virtual void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
