using MediatR;
using Shared.Domain.Abstractions;
using System;

namespace User.Web.Application.Command
{
    public class CreateUserCommand : Entity<Guid>, IRequest
    {

    }
}
