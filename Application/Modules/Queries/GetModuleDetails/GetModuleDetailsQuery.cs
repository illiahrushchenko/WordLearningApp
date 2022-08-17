using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Modules.DTOs;

namespace Application.Modules.Queries.GetModuleDetails
{
    public class GetModuleDetailsQuery : IRequest<ModuleDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetModuleDetailsQueryHandler : IRequestHandler<GetModuleDetailsQuery, ModuleDetailsDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetModuleDetailsQueryHandler(ApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
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
                module.OwnerId != _currentUserService.UserId)
            {
                throw new PermissionDeniedException(_currentUserService.UserId);
            }

            return _mapper.Map<ModuleDetailsDto>(module);
        }
    }
}
