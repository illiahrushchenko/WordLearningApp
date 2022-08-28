using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LearningProgressItems.Commands.AddWritingAnswer
{
    public class AddWritingAnswerCommandValidator : AbstractValidator<AddWritingAnswerCommand>
    {
        public AddWritingAnswerCommandValidator()
        {
            RuleFor(x => x.LearningProgressItemId)
                .GreaterThan(0).WithMessage("ModuleId must be > 0");
        }
    }
}
