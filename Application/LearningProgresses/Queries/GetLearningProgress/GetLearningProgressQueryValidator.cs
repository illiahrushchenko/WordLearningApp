using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LearningProgresses.Queries.GetLearningProgress
{
    public class GetLearningProgressQueryValidator : AbstractValidator<GetLearningProgressQuery>
    {
        public GetLearningProgressQueryValidator()
        {
            RuleFor(x => x.ModuleId)
                .GreaterThan(0).WithMessage("ModuleId must be greater than 0");
        }
    }
}
