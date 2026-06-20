using CatalogService.Application.Extentions;
using CatalogService.Application.Features.Book.IntegrationEvents;
using CatalogService.Application.Features.Book.OrderEvent.EventModels;
using CatalogService.Application.Messaging;
using CatalogService.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Application;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApplicationLayer(this IServiceCollection services)
    {
        services
            .RegisterMediatR()
            .RegisterCurrentUser()
            .RegisterIntegrationEventHandlers();

        return services;
    }

    private static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }

    private static IServiceCollection RegisterCurrentUser(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<ICurrentUser, CurrentUser>();

        return services;
    }

    private static IServiceCollection RegisterIntegrationEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventHandler<OrderCreatedEvent>, OrderCreatedEventHandler>();

        return services;
    }
}
