using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using StructureMap;

namespace ViewModeler.Validation
{
    public class FluentValidationProvider: IValidationProvider
    {
        static Container container = new Container(cfg => cfg.AddRegistry<ValidationRegistry>());
         
        public IValidationResult Validate<T>(T instance)
        {
            var validator = LocateValidator(instance);

            return ValidateModel(instance, validator);
        }

        IValidationResult ValidateModel<T>(T instance, IValidator validator)
        {
            if (validator != null)
            {
                return new FluentValidationResult(validator.Validate(instance));
            }
            return FluentValidationResult.Positive();
        }

        IValidator LocateValidator<T>(T instance)
        {
            var concreteInterfaceType = typeof (IValidator<>).MakeGenericType(instance.GetType());
            var validator = container.TryGetInstance(concreteInterfaceType) as IValidator;
            return validator;
        }
    }
}
