﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LearningProgressItems.Queries.GetNextWritingQuestion
{
    public class GetNextWritingQuestionQueryValidator : AbstractValidator<GetNextWritingQuestionQuery>
    {
        public GetNextWritingQuestionQueryValidator()
        {
            RuleFor(x => x.ModuleId)
                .GreaterThan(0).WithMessage("ModuleId must be > 0");
        }
    }
}