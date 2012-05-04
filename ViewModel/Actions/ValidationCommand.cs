using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.IoC;
using ViewModel.Models;

namespace ViewModel.Actions
{
    /// <summary>
    /// Represents operation that validates and then pushes data to the server
    /// </summary>
    public class ValidationCommand: Command
    {
        public ValidationCommand(Action<object> execute, Predicate<object> canExecute = null) : base(execute, canExecute)
        {
        }

        protected override bool BeforeCanExecuteHappened()
        {
            var validationProvider = IoCContainer.Resolver.TryGetInstance<IValidationProvider>();
            var isModelValid = validationProvider.Validate(viewModel).IsModelValid();
            return isModelValid;
        }
    }
}
