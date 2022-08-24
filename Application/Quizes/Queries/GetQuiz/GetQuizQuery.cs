using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Quizes.DTOs;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Quizes.Queries.GetQuiz
{
    public class GetQuizQuery : IRequest<QuizDto>
    {
        public int ModuleId { get; set; }
        public int CardNumber { get; set; }
    }

    public class GetQuizQueryHandler : IRequestHandler<GetQuizQuery, QuizDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetQuizQueryHandler(ICurrentUserService currnetUserService, ApplicationDbContext context)
        {
            _currentUserService = currnetUserService;
            _context = context;
        }

        public async Task<QuizDto> Handle(GetQuizQuery request, CancellationToken cancellationToken)
        {
            var module = await _context.Modules
                .Include(x => x.Words)
                .FirstOrDefaultAsync(x => x.Id == request.ModuleId, cancellationToken);

            if (module == null)
            {
                throw new NotFoundException(nameof(module), request.ModuleId);
            }

            if (!module.IsPublic &&
                module.OwnerId != _currentUserService.UserId)
            {
                throw new PermissionDeniedException(_currentUserService.UserId);
            }

            if ((module.Words.Count - 1) < request.CardNumber)
            {
                throw new NotFoundException("CardNumber is too big.");
            }

            var card = module.Words[request.CardNumber];

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
