using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    public interface IValidationProvider
    {
        IValidationResult Validate<T>(T instance);
    }

    public interface IValidationResult
    {
        IEnumerable<String> GetErrorsForProperty(string propertyName);
        bool IsModelValid();
        /// <summary>
        /// Retrieves total error count from all over the validated object
        /// </summary>
        int GetErrorCount();
    }
}
