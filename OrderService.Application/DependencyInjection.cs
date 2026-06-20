using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Features.Orders.StockResultEvents;
using OrderService.Application.Features.Orders.StockResultEvents.EventModels;
using OrderService.Application.Messaging;

namespace OrderService.Application;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddScoped<IIntegrationEventHandler<StockReservedEvent>, StockReservedEventHandler>();
        services.AddScoped<IIntegrationEventHandler<StockFailedEvent>, StockFailedEventHandler>();
        return services;
    }
}
