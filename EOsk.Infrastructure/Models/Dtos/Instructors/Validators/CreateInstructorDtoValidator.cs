using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EOsk.Infrastructure.Models.Dtos.Instructors.Validators.Common;
using FluentValidation;

namespace EOsk.Infrastructure.Models.Dtos.Instructors.Validators
{
    public class CreateInstructorDtoValidator : AbstractValidator<CreateInstructorDto>
    {
        public CreateInstructorDtoValidator()
        {
            Include(new IInstructorDtoValidator());
        }
    }
}
