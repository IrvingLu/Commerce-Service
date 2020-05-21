using Order.Application.IntegretionEvents.Events;
using System.Threading.Tasks;

namespace Order.Application.IntegretionEvents.EventHanding
{
    public  interface ISubscriberService
    {
        Task UpdateStatusAsync(OrderChangeEvents @event);
    }
}
