using Application.Common.Exceptions;
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
        public string UserEmail { get; set; }
        public int ModuleId { get; set; }
    }

    public class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public async Task<Unit> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {
            var userId = (await _userManager.FindByEmailAsync(request.UserEmail)).Id;

            var module = await _context.Modules
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.ModuleId);

            if(module == null)
            {
                throw new NotFoundException(nameof(module), request.ModuleId);
            }

            if (module.OwnerId != userId)
            {
                throw new PermissionDeniedException(request.UserEmail);
            }

            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
