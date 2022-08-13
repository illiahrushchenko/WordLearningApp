using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Modules.Commands.DeleteModule
{
    public class DeleteModuleCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand>
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteModuleCommandHandler(ICurrentUserService currentUserService, ApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<Unit> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {
            var module = await _context.Modules
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if(module == null)
            {
                throw new NotFoundException(nameof(module), request.Id);
            }

            if (module.OwnerId != _currentUserService.UserId)
            {
                throw new PermissionDeniedException(_currentUserService.UserId);
            }

            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
