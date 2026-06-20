
using CatalogService.Application.Features.Book.OrderEvent.EventModels;
using CatalogService.Application.Messaging;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.BusinessIRepositories;
using CatalogService.Infrastructure.BusinessRepositories;
using CatalogService.Infrastructure.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrustructorLayer(this IServiceCollection services , IConfiguration configuration)
    {
        services
            .RegisterDBContext(configuration)
            .RegisterRedisCache(configuration)
            .RegisterRepositories()
            .RegisterRabbitMqConsumers();


        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IEventBus, RabbitMqEventBus>();
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IBookRepository), typeof(BookRepository));

        return services;
    }

    private static IServiceCollection RegisterDBContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogDBContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("BookOrderManagementDB"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
            );

            sqlOptions.MigrationsHistoryTable("__CatalogMigrationsHistory");
        }
    ));


        return services;
    }

    private static IServiceCollection RegisterRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis") ?? "localhost:6379";
            options.InstanceName = "CatalogService:";
        });

        return services;
    }

    private static IServiceCollection RegisterRabbitMqConsumers(this IServiceCollection services)
    {
        services.AddHostedService<RabbitMqEventConsumer<OrderCreatedEvent>>();

        return services;
    }


}
