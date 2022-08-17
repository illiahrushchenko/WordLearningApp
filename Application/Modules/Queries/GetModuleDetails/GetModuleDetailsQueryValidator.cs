using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Modules.Queries.GetModuleDetails
{
    public class GetModuleDetailsQueryValidator : AbstractValidator<GetModuleDetailsQuery>
    {
        public GetModuleDetailsQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}
