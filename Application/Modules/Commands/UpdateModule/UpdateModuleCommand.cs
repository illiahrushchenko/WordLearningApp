using Application.Common.Exceptions;
using AutoMapper;
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

namespace Application.Modules.Commands.UpdateModule
{
    public class UpdateModuleCommand : IRequest<int>
    {
        public string UserEmail { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool IsPublic { get; set; }

        public List<UpdateCardCommand> Words { get; set; }
    }

    public class UpdateCardCommand
    {
        public string Term { get; set; }
        public string Definition { get; set; }
    }

    public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand, int>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UpdateModuleCommandHandler(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<int> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            var module = await _context.Modules
                .Include(x => x.Words)
                .FirstOrDefaultAsync(x => x.Id == request.ModuleId);

            if(module == null)
            {
                throw new NotFoundException(nameof(module), module.Id);
            }

            if (module.OwnerId != (await _userManager.FindByEmailAsync(request.UserEmail)).Id)
            {
                throw new PermissionDeniedException(request.UserEmail);
            }

            module.Name = request.Name;
            module.Note = request.Note;
            module.IsPublic = request.IsPublic;

            for (int i = 0; i < module.Words.Count; i++)
            {
                module.Words[i].Term = request.Words[i].Term;
                module.Words[i].Definition = request.Words[i].Definition;
            }

            await _context.SaveChangesAsync();

            return module.Id;
        }
    }
}
