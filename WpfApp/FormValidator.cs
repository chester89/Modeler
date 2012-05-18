using System;
using System.Linq;
using FluentValidation;
using ViewModeler.Models;

namespace WpfPostSharpTesting
{
    public class FormValidator: AbstractValidator<FormViewModel>
    {
        public FormValidator()
        {
            RuleFor(m => m.Text).Length(7, 10).WithMessage("Text length should be between 7 and 10");
            RuleFor(m => m.Title).Length(12).WithMessage("Title must be precisely 12 characters long");
        }
    }
}
