using CatalogService.Application.Messaging;
using CatalogService.Application.UseCases.Book.OrderEvent.EventModels;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.Repositories.BusinessIRepositories.BookRepository;
using CatalogService.Infrastructure.Messaging;
using CatalogService.Infrastructure.Repositories.BusinessRepositories.BookRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure.DI;
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
        services.AddScoped(typeof(IBookRepositoryQuery), typeof(BookRepositoryCommond));

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
