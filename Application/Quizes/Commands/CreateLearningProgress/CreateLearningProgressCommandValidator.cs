using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Quizes.Commands.CreateLearningProgress
{
    public class CreateLearningProgressCommandValidator : AbstractValidator<CreateLearningProgressCommand>
    {
        public CreateLearningProgressCommandValidator()
        {
            RuleFor(x => x.ModuleId)
                .GreaterThan(0).WithMessage("ModuleId must be greater than 0");
        }
    }
}
