using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Application.Common.Exceptions;

namespace Application.Modules.Queries.GetModuleDetails
{
    public class GetModuleDetailsQuery : IRequest<ModuleDetailsDto>
    {
        public string UserEmail { get; set; }
        public int Id { get; set; }
    }

    public class GetModuleDetailsQueryHandler : IRequestHandler<GetModuleDetailsQuery, ModuleDetailsDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public GetModuleDetailsQueryHandler(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ModuleDetailsDto> Handle(GetModuleDetailsQuery request, CancellationToken cancellationToken)
        {
            var module = await _context.Modules
                .AsNoTracking()
                .Include(x => x.Words)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if(module == null)
            {
                throw new NotFoundException(nameof(module), request.Id);
            }

            if(!module.IsPublic &&
                module.OwnerId != (await _userManager.FindByEmailAsync(request.UserEmail)).Id)
            {
                throw new PermissionDeniedException(request.UserEmail);
            }

            return _mapper.Map<ModuleDetailsDto>(module);
        }
    }
}
