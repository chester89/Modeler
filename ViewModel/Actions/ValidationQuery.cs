using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.IoC;
using ViewModel.Models;

namespace ViewModel.Actions
{
    /// <summary>
    /// Represents operation that validates and then retrieves data to the server
    /// </summary>
    public class ValidationQuery: Query
    {
        public ValidationQuery(Action<object> execute, Predicate<object> canExecute, ViewModelBase viewModelInstance): 
            base(execute, canExecute, viewModelInstance)
        {
        }

        protected override bool BeforeCanExecuteHappened()
        {
            return IoCContainer.Resolver.TryGetInstance<IValidationProvider>().Validate(viewModel).IsModelValid();
        }
    }
}
