using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.LearningProgresses.DTOs;
using AutoMapper;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.LearningProgresses.Queries.GetLearningProgress
{
    public class GetLearningProgressQuery : IRequest<LearningProgressDto>
    {
        public int ModuleId { get; set; }
    }

    public class GetLearningProgressQueryHandler : IRequestHandler<GetLearningProgressQuery, LearningProgressDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetLearningProgressQueryHandler(ApplicationDbContext context, ICurrentUserService currentUserService, IMapper mapper)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<LearningProgressDto> Handle(GetLearningProgressQuery request, CancellationToken cancellationToken)
        {
            var learningProgress = await _context.LearningProgresses
                .Include(x => x.LearningProgressItems)
                .FirstOrDefaultAsync(x =>
                    x.UserId == _currentUserService.UserId && x.ModuleId == request.ModuleId,
                    cancellationToken);

            if (learningProgress == null)
            {
                throw new NotFoundException($"No LearningProgress with UserId {_currentUserService.UserId} and ModuleId {request.ModuleId}");
            }

            return _mapper.Map<LearningProgressDto>(learningProgress);
        }
    }
}
