using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Quizes.Commands.AddWritingAnswer
{
    public class AddWritingAnswerCommand : IRequest
    {

    }

    public class AddWritingAnswerCommandHandler : IRequestHandler<AddWritingAnswerCommand>
    {
        public Task<Unit> Handle(AddWritingAnswerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
