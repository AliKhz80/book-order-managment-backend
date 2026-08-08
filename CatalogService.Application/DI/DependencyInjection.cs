using CatalogService.Application.Messaging;
using CatalogService.Application.UseCases.Book.OrderEvent;
using CatalogService.Application.UseCases.Book.OrderEvent.EventModels;
using CatalogService.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApplicationLayer(this IServiceCollection services)
    {
        services
            .RegisterMediatR()
            .RegisterIntegrationEventHandlers();

        return services;
    }

    private static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }

   

    private static IServiceCollection RegisterIntegrationEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventHandler<OrderCreatedEvent>, OrderCreatedEventHandler>();

        return services;
    }
}
