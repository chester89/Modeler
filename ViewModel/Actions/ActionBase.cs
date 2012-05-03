using System;
using System.Windows.Input;
using ViewModel.Aspects;
using ViewModel.Models;

namespace ViewModel.Actions
{
    /// <summary>
    /// Provides basic implementation of <see cref="ICommand"/> interface
    /// </summary>
    public class ActionBase: ICommand
    {
        protected readonly Predicate<Object> canExecute;
        protected readonly Action<Object> execute;
        protected bool? LastCanExecuteChangedValue;
        protected ViewModelBase viewModel;

        public ActionBase(Action<Object> execute, Predicate<Object> canExecute = null)
        {
            this.canExecute = canExecute;
            this.execute = execute;
            LastCanExecuteChangedValue = null;
        }

        public void SetViewModel(ViewModelBase viewModel)
        {
            if (viewModel != null)
            {
                this.viewModel = viewModel;
                SubscribeViewModelToCanExecuteChange();
            }
            else
            {
                throw new ArgumentException("viewModel parameter should not be null");
            }
        }

        protected void SubscribeViewModelToCanExecuteChange()
        {
            viewModel.PropertyChanged += (e, a) =>
                                             {
                                                 bool canNowExecute = CanExecute(null);
                                                 //If command can execute at given moment AND canExecute value has changed since its last evaluation
                                                 if (canNowExecute && (canNowExecute != LastCanExecuteChangedValue))
                                                 {
                                                     NotifyCanExecuteChanged();
                                                 }
                                             };
        }

        public bool CanExecute(object parameter)
        {
            bool beforeCanExecuteResult = BeforeCanExecuteHappened();
            if (canExecute != null)
            {
                LastCanExecuteChangedValue = canExecute(parameter);
                return beforeCanExecuteResult && LastCanExecuteChangedValue.Value;
            }
            return beforeCanExecuteResult;
        }

        /// <summary>
        /// Runs before CanExecute method of a command is executed. This is a point for invoking some validation on ViewModel
        /// </summary>
        protected virtual bool BeforeCanExecuteHappened()
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public virtual void NotifyCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                viewModel.OnUiThread(() => CanExecuteChanged(this, EventArgs.Empty) );
            }
        }

        public static ICommand Empty 
        { 
            get
            {
                return new ActionBase(par => { });
            }
        }

        [BackgroundThreadAspect]
        public void Execute(object parameter)
        {
            var context = new ExitContext();
            try
            {
                OnExecuteEntry();
                execute(parameter);
            }
            catch (Exception ex)
            {
                context.AddException(ex);
            }
            finally
            {
                OnExecuteExit(context);
            }
        }

        /// <summary>
        /// Runs before Execute call
        /// </summary>
        protected virtual void OnExecuteEntry()
        {
            
        }

        /// <summary>
        /// Runs after Execute call even if it raised exceptions in process
        /// </summary>
        protected virtual void OnExecuteExit(ExitContext context)
        {

        }
    }
}