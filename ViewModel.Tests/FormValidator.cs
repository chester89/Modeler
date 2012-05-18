using FluentValidation;
using ViewModeler.Models;
using ViewModeler.Tests;

namespace ViewModel
{
    public class FormValidator: AbstractValidator<FormViewModel>
    {
        public FormValidator()
        {
            RuleFor(m => m.Text).Length(3, 10).WithMessage("Text length should be between 3 and 10");
            RuleFor(m => m.Title).Length(12).WithMessage("Title must be precisely 12 characters long");
        }
    }
}
