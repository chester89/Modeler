using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ViewModel.Validation
{
    public class DataAnnotationsValidationProvider: IValidationProvider
    {
        public IValidationResult Validate<T>(T instance)
        {
            var result = new DataAnnotationValidationResult();

            var properties = instance.GetType().GetProperties();

            foreach (var propertyInfo in properties)
            {
                var validationAttributes = propertyInfo.GetCustomAttributes(true).OfType<ValidationAttribute>();

                foreach (var validationAttribute in validationAttributes)
                {
                    if (!validationAttribute.IsValid(propertyInfo.GetValue(instance, null)))
                    {
                        result.AddError(propertyInfo.Name, validationAttribute.ErrorMessage);
                    }
                }
            }

            return result;
        }
    }
}
