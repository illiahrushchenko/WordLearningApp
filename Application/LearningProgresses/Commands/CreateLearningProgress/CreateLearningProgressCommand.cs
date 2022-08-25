using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.LearningProgresses.Commands.CreateLearningProgress
{
    public class CreateLearningProgressCommand : IRequest
    {
        public int ModuleId { get; set; }
    }

    public class CreateLearningProgressHandler : IRequestHandler<CreateLearningProgressCommand>
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateLearningProgressHandler(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(CreateLearningProgressCommand request, CancellationToken cancellationToken)
        {
            var module = await _context.Modules
                .Include(x => x.Words)
                .FirstOrDefaultAsync(x => x.Id == request.ModuleId);

            if (module == null)
            {
                throw new NotFoundException(nameof(module), request.ModuleId);
            }

            var learningProgress = new LearningProgress
            {
                ModuleId = module.Id,
                UserId = _currentUserService.UserId,

                LearningProgressItems = new List<LearningProgressItem>()
            };

            foreach (var card in module.Words)
            {
                learningProgress.LearningProgressItems.Add(new LearningProgressItem
                {
                    CardId = card.Id
                });
            }

            await _context.AddAsync(learningProgress, cancellationToken);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
