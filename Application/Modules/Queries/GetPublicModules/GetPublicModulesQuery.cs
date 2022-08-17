using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Modules.DTOs;

namespace Application.Modules.Queries.GetPublicModules
{
    public class GetPublicModulesQuery : IRequest<PaginatedList<ModuleBriefDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetPublicModulesQueryHandler : IRequestHandler<GetPublicModulesQuery, PaginatedList<ModuleBriefDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPublicModulesQueryHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ModuleBriefDto>> Handle(GetPublicModulesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Modules.
                Where(x => x.IsPublic == true)
                .OrderBy(x => x.Name)
                .ProjectTo<ModuleBriefDto>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
