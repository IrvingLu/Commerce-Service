using DotNetCore.CAP;
using Order.Application.IntegretionEvents.Events;
using Shared.Infrastructure.Core.Repository;
using System;
using System.Threading.Tasks;

namespace Order.Application.IntegretionEvents.EventHanding
{
    public class SubscriberService : ISubscriberService,ICapSubscribe
    {
        private readonly IRepository<Core.Domain.Order,Guid> _orderRepository;
       // private readonly IDapperQueries<Core.Domain.Order, Guid> _dapperQueries;

        #region Ctor
        public SubscriberService(IRepository<Core.Domain.Order, Guid> orderRepository /*IDapperQueries<Core.Domain.Order, Guid> dapperQueries*/)
        {
            _orderRepository = orderRepository;
          //  _dapperQueries = dapperQueries;
        }
        [CapSubscribe("order.services.change.status")]
        public async Task UpdateStatusAsync(OrderChangeEvents @event)
        {
            //var result = await _orderRepository.TableNoTracking.Where(c => c.Id == @event.OrderId).FirstOrDefaultAsync();
            //result.OrderStatus = 2;
            //await _orderRepository.UpdateAsync(result);

        }
        #endregion
    }
}
