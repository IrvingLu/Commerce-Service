using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using User.Web.Application.Command;

namespace User.Web.Application.CommandHandler
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
