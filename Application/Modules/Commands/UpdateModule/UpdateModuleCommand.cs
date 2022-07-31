using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Modules.Commands.UpdateModule
{
    public class UpdateModuleCommand : IRequest<int>
    {
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }

        public List<UpdateCardCommand> Words { get; set; }
    }

    public class UpdateCardCommand
    {
        public string Term { get; set; }
        public string Definition { get; set; }
    }

    public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand, int>
    {
        public Task<int> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
