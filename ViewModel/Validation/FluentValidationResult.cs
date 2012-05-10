using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;

namespace ViewModeler.Validation
{
    public class FluentValidationResult : IValidationResult
    {
        readonly ValidationResult result;

        public FluentValidationResult(ValidationResult result)
        {
            this.result = result;
        }

        public IEnumerable<String> GetErrorsForProperty(string propertyName)
        {
            if (!result.IsValid)
            {
                var errors = result.Errors.Where(fail => fail.PropertyName == propertyName);
                return errors.Select(er => er.ErrorMessage);
            }
            return new List<String>();
        }

        public static IValidationResult Positive()
        {
            return new FluentValidationResult(new ValidationResult());
        }

        public bool IsModelValid()
        {
            return result.IsValid;
        }
        
        public int GetErrorCount()
        {
            if (result.Errors != null && result.Errors.Any())
                return result.Errors.Count;
            return 0;
        }
    }
}
