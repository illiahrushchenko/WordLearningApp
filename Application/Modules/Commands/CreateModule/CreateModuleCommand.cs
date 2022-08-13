using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Modules.Commands.CreateModule
{
    public class CreateModuleCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public bool IsPublic { get; set; }

        public List<CreateCardCommand> Words { get; set; }
    }

    public class CreateCardCommand
    {
        public string Term { get; set; }
        public string Definition { get; set; }
    }

    public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public CreateModuleCommandHandler(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            var module = _mapper.Map<Module>(request);

            var user = await _userManager.FindByEmailAsync(_currentUserService.UserEmail);

            if(user == null)
            {
                throw new NotFoundException(nameof(user), _currentUserService.UserId );
            }

            module.OwnerId = user.Id;

            await _context.Modules.AddAsync(module, cancellationToken);
            await _context.SaveChangesAsync();

            return module.Id;
        }
    }
}
