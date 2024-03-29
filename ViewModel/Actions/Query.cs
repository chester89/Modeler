﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModeler.Models;

namespace ViewModeler.Actions
{
    /// <summary>
    /// Provides base class for actions that performs readonly operations on a datasource
    /// </summary>
    public class Query: ActionBase
    {
        public Query(Action<object> execute, Predicate<object> canExecute = null) : base(execute, canExecute)
        {
            
        }

        protected override void OnExecuteEntry()
        {
            viewModel.SetState(ViewModelState.Loading);

            base.OnExecuteEntry();
        }

        protected override void OnExecuteExit(ExitContext context)
        {
            viewModel.SetState(context.ExceptionsOccured ? ViewModelState.Faulted : ViewModelState.Still);

            base.OnExecuteExit(context);
        }
    }
}
