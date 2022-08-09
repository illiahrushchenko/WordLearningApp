using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Modules.Commands.CreateModule
{
    public class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
    {
        public CreateModuleCommandValidator()
        {
            RuleFor(x => x.Words)
                .NotEmpty().WithMessage("Words are required");
            RuleFor(x => x.IsPublic)
                .NotNull().WithMessage("IsPublic property is required");
            RuleFor(x => x.Words)
                .Must(x => x.Count >= 2).WithMessage("At least 2 words are required");
            RuleFor(x => x.Words)
                .Must(x => x.Count <= 30).WithMessage("No more than 30 words allowed");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");
        }
    }
}
