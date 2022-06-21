using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using EOsk.Infrastructure.Extensions;

namespace EOsk.Infrastructure.Models.Dtos.Instructors.Validators
{
    public class IInstructorDtoValidator : AbstractValidator<IInstructorDto>
    {
        public IInstructorDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("{PropertyName} is required.")
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(3).WithMessage("{PropertyName} must have more than {PropertyValue} characters.");

            RuleFor(x=>x.LastName)
                .NotNull().WithMessage("{PropertyName} is required.")
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(2).WithMessage("{PropertyName} must have more than {PropertyValue} characters.");

            RuleFor(x => x.Pesel)
                .NotNull().WithMessage("{PropertyName} is required.")
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MustBePesel().WithMessage("The string provided is not a valid pesel number: {PropertyValue}");

            RuleFor(x => x.EndOfValidityOfTheCard)
                .GreaterThan(DateTime.Now).WithMessage("{PropertyName} must be greater than today.");
        }
    }
}
