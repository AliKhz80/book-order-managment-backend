using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Messaging;
using OrderService.Application.UseCases.Order.StockResultEvents;
using OrderService.Application.UseCases.Order.StockResultEvents.EventModels;

namespace OrderService.Application.DI;

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
