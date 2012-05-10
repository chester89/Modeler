using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModeler.Models;

namespace ViewModeler.Actions
{
    /// <summary>
    /// Provides base class for actions that post data to some data-source
    /// </summary>
    public class Command: ActionBase
    {
        public Command(Action<object> execute, Predicate<object> canExecute = null) : base(execute, canExecute)
        {
        }

        protected override void OnExecuteEntry()
        {
            base.OnExecuteEntry();

            viewModel.SetState(ViewModelState.Uploading);
        }

        protected override void OnExecuteExit(ExitContext context)
        {
            viewModel.SetState(context.ExceptionsOccured ? ViewModelState.Faulted : ViewModelState.Still);

            base.OnExecuteExit(context);
        }
    }
}
