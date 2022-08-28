using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LearningProgressItems.Commands.AddQuizAnswer
{
    public class AddQuizAnswerCommandValidator : AbstractValidator<AddQuizAnswerCommand>
    {
        public AddQuizAnswerCommandValidator()
        {
            RuleFor(x => x.LearningProgressItemId)
                .GreaterThan(0).WithMessage("ModuleId must be > 0");
        }
    }
}
