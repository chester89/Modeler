using System;
using System.ComponentModel;
using System.Windows.Input;
using ViewModeler.Aspects;
using ViewModeler.Actions;
using ViewModeler.Infrastructure;
using ViewModeler.IoC;

namespace ViewModeler.Models
{
    /// <summary>
    /// Base class for a view model - provides property notification support, basic validation
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
        protected IMessenger Messenger;

        public static void SetDispatcher(IDispatcher appDispatcher)
        {
            Dispatcher = appDispatcher;
        }

        private void Init()
        {
            IsValidationOn = true;
            State = ViewModelState.Still;
        }

        protected ViewModelBase()
        {
            Init();
            Messenger = IoCContainer.Resolver.TryGetInstance<IMessenger>();
        }

        protected ViewModelBase(IMessenger messenger)
        {
            Init();
            Messenger = messenger;
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

        public void Select()
        {
            IsSelected = true;
        }

        private void OnParentSet(ViewModelBase newParentValue)
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
        /// Executes provided <paramref name="action"/> on UI thread
        /// </summary>
        /// <param name="action"></param>
        public static void RunInUi(Action action)
        {
            if (Dispatcher.OnUiThread)
                action();
            else
                Dispatcher.Invoke(action);
        }

        /// <summary>
        /// Executes <paramref name="action"/> on UI thread
        /// </summary>
        public virtual void OnUiThread(Action action)
        {
            RunInUi(action);
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
