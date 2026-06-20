using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Messaging
{
    public interface IIntegrationEvent
    {
    }

    public interface IIntegrationEventHandler<in TEvent> where TEvent : IIntegrationEvent
    {
        Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
    }

    public interface IEventDispatcher
    {
        Task DispatchAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : IIntegrationEvent;
    }
}
