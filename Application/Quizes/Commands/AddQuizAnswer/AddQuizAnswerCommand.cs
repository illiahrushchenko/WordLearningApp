using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Quizes.Commands.AddQuizAnswer
{
    public class AddQuizAnswerCommand : IRequest
    {
        public int CardId { get; set; }
        public string Answer { get; set; }
    }

    public class AddQuizAnswerCommandHandler : IRequestHandler<AddQuizAnswerCommand>
    {
        public Task<Unit> Handle(AddQuizAnswerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
