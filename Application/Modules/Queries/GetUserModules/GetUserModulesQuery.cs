using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Modules.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Modules.Queries.GetUserModules
{
    public class GetUserModulesQuery : IRequest<PaginatedList<ModuleBriefDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetUserModulesQueryHandler : IRequestHandler<GetUserModulesQuery, PaginatedList<ModuleBriefDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetUserModulesQueryHandler(IMapper mapper, ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<PaginatedList<ModuleBriefDto>> Handle(GetUserModulesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Modules
                .Where(x => x.OwnerId == _currentUserService.UserId)
                .OrderBy(x => x.Name)
                .ProjectTo<ModuleBriefDto>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
