using Application.Common.Interfaces;
using Application.LearningProgressItems.DTOs;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;

namespace Application.LearningProgressItems.Queries.GetNextQuizQuestion
{
    public class GetNextQuizQuery : IRequest<QuizDto>
    {
        public int ModuleId { get; set; }
    }

    public class GetNextQuizQueryHandler : IRequestHandler<GetNextQuizQuery, QuizDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetNextQuizQueryHandler(ICurrentUserService currentUserService, ApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<QuizDto> Handle(GetNextQuizQuery request, CancellationToken cancellationToken)
        {
            var learningProgress = await _context.LearningProgresses
                .Include(x => x.LearningProgressItems)
                    .ThenInclude(x => x.Card)
                .Include(x => x.Module)
                .FirstOrDefaultAsync(x =>
                    x.UserId == _currentUserService.UserId && x.ModuleId == request.ModuleId,
                    cancellationToken);

            if(learningProgress == null)
            {
                throw new NotFoundException($"No LearningProgress with UserId {_currentUserService.UserId} and ModuleId {request.ModuleId}");
            }

            var card = learningProgress.LearningProgressItems
                .FirstOrDefault(x => !x.AnsweredWithQuiz)
                .Card;

            if(card == null)
            {
                throw new NotFoundException("All quizes are alredy answered");
            }

            var module = learningProgress.Module;

            if (!module.IsPublic &&
                module.OwnerId != _currentUserService.UserId)
            {
                throw new PermissionDeniedException(_currentUserService.UserId);
            }

            var cardDefinitions = module.Words
                .Select(x => x.Definition)
                .ToList();

            var options = new List<string>();

            options.Add(card.Definition);
            cardDefinitions.Remove(card.Definition);

            var random = new Random();

            while (options.Count < 4 && cardDefinitions.Any())
            {
                var randomDefinition = cardDefinitions[random.Next(cardDefinitions.Count)];
                options.Add(randomDefinition);
                cardDefinitions.Remove(randomDefinition);
            }

            return new QuizDto
            {
                Term = card.Term,
                CardId = card.Id,
                Options = options
            };
        }
    }
}
