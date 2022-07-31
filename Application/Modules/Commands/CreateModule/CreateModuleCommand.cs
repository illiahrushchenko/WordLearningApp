using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Modules.Commands.CreateModule
{
    public class CreateModuleCommand : IRequest<int>
    {
        public string OwnerEmail { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }

        public List<CreateCardCommand> Words { get; set; }
    }

    public class CreateCardCommand
    {
        public string Term { get; set; }
        public string Definition { get; set; }
    }

    public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand, int>
    {
        public Task<int> Handle(CreateModuleCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
