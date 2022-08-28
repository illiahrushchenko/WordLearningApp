using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.LearningProgressItems.DTOs;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.LearningProgressItems.Queries.GetNextWritingQuestion
{
    public class GetNextWritingQuestionQuery : IRequest<WritingQuestionDto>
    {
        public int ModuleId { get; set; }
    }

    public class GetNextWritingQuestionQueryHandler : IRequestHandler<GetNextWritingQuestionQuery, WritingQuestionDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetNextWritingQuestionQueryHandler(ICurrentUserService currentUserService, ApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<WritingQuestionDto> Handle(GetNextWritingQuestionQuery request, CancellationToken cancellationToken)
        {
            var learningProgress = await _context.LearningProgresses
                .AsNoTracking()
                .Include(x => x.LearningProgressItems)
                    .ThenInclude(x => x.Card)
                .FirstOrDefaultAsync(x =>
                    x.UserId == _currentUserService.UserId && x.ModuleId == request.ModuleId,
                    cancellationToken);

            if (learningProgress == null)
            {
                throw new NotFoundException($"No LearningProgress with UserId {_currentUserService.UserId} and ModuleId {request.ModuleId}");
            }

            var learningProgressItem = learningProgress.LearningProgressItems
                .FirstOrDefault(x => !x.AnsweredWithWriting);

            if (learningProgressItem == null)
            {
                throw new NotFoundException("All quizes are alredy answered");
            }

            var card = learningProgressItem.Card;

            return new WritingQuestionDto
            {
                LearningProgressItemId = learningProgressItem.Id,
                Term = card.Term,
                CorrectAnswer = card.Definition
            };
        }
    }
}
