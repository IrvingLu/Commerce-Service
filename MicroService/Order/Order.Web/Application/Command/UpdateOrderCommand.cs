using MediatR;
using Shared.Domain.Abstractions;
using System;

namespace Order.Web.Application.Command
{
    public class UpdateOrderCommand : Entity<Guid>, IRequest
    {
    }
}
