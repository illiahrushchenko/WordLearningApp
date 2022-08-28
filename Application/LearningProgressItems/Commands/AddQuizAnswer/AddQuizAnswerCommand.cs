using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.LearningProgressItems.Commands.AddQuizAnswer
{
    public class AddQuizAnswerCommand : IRequest
    {
        public int LearningProgressItemId { get; set; }
    }

    public class AddQuizAnswerCommandHandler : IRequestHandler<AddQuizAnswerCommand>
    {
        private readonly ApplicationDbContext _context;

        public AddQuizAnswerCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddQuizAnswerCommand request, CancellationToken cancellationToken)
        {
            var learningProgressItem = await _context.LearningProgressItems
                .FirstOrDefaultAsync(x => x.Id == request.LearningProgressItemId, cancellationToken);

            learningProgressItem.AnsweredWithQuiz = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
