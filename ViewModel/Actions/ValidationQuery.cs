using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.IoC;
using ViewModel.Models;

namespace ViewModel.Actions
{
    /// <summary>
    /// Represents operation that validates viewModel and, if model is valid, reaches for datasource
    /// </summary>
    public class ValidationQuery: Query
    {
        public ValidationQuery(Action<object> execute, Predicate<object> canExecute = null): base(execute, canExecute)
        {
        }

        protected override bool BeforeCanExecuteHappened()
        {
            return IoCContainer.Resolver.TryGetInstance<IValidationProvider>().Validate(viewModel).IsModelValid();
        }
    }
}
