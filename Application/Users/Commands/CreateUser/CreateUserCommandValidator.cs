using Application.Common.Services.Abstractions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Equal(x => x.PasswordConfirm).WithMessage("Passwords do not match");
        }
    }
}
