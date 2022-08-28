using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.LearningProgressItems.Commands.AddWritingAnswer
{
    public class AddWritingAnswerCommand : IRequest
    {
        public int LearningProgressItemId { get; set; }
    }

    public class AddWritingAnswerCommandHandler : IRequestHandler<AddWritingAnswerCommand>
    {
        private readonly ApplicationDbContext _context;

        public AddWritingAnswerCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddWritingAnswerCommand request, CancellationToken cancellationToken)
        {
            var learningProgressItem = await _context.LearningProgressItems
                .FirstOrDefaultAsync(x => x.Id == request.LearningProgressItemId, cancellationToken);

            learningProgressItem.AnsweredWithWriting = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
