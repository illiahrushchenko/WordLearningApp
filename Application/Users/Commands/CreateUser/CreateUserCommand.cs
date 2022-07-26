using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly UserManager<User> _userManager;

        public CreateUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateAsync(new User
            {
                UserName = request.UserName,
                Email = request.Email
            }, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToArray();
                throw new ValidationException(errors);
            }

            return Unit.Value;
        }
    }
}
