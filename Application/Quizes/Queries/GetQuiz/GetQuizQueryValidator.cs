using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Quizes.Queries.GetQuiz
{
    public class GetQuizQueryValidator : AbstractValidator<GetQuizQuery>
    {
        public GetQuizQueryValidator()
        {
            RuleFor(x => x.CardNumber)
                .GreaterThanOrEqualTo(0).WithMessage("CardNumber must be positive");
            RuleFor(x => x.ModuleId)
                .GreaterThan(0).WithMessage("ModuleId must be 1 or more");
        }
    }
}
