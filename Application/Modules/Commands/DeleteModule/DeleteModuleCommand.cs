using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Modules.Commands.DeleteModule
{
    public class DeleteModuleCommand : IRequest
    {
        public int ModuleId { get; set; }
    }

    class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand>
    {
        public Task<Unit> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
