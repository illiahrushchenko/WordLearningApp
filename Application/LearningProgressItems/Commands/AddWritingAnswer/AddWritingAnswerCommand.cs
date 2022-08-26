using MediatR;
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
        public int CardId { get; set; }
        public string Answer { get; set; }
    }

    public class AddWritingAnswerCommandHandler : IRequestHandler<AddWritingAnswerCommand>
    {
        public Task<Unit> Handle(AddWritingAnswerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
