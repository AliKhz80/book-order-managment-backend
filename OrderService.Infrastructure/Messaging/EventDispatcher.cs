using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Messaging
{
    public class EventDispatcher(IServiceScopeFactory serviceScopeFactory) : IEventDispatcher
    {
        public async Task DispatchAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : IIntegrationEvent
        {
            // 1. Create a fresh scope for this specific message
            using var scope = serviceScopeFactory.CreateScope();

            // 2. Ask the Service Provider for the specific handler for this event type
            var handler = scope.ServiceProvider.GetRequiredService<IIntegrationEventHandler<TEvent>>();

            // 3. Execute the handler
            await handler.HandleAsync(@event, cancellationToken);
        }
    }
}
