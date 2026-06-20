using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Features.Orders.StockResultEvents.EventModels;
using OrderService.Application.Messaging;
using OrderService.Domain.Interfaces;
using OrderService.Infrastructure.Messaging;
using OrderService.Infrastructure.Repositories;

namespace OrderService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .RegisterDbContext(configuration)
            .RegisterRepositories()
            .RegisterRabbitMqConsumers();


        return services;
    }

    private static IServiceCollection RegisterDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("BookOrderManagementDB"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
            );

            sqlOptions.MigrationsHistoryTable("__OrderMigrationsHistory");
        }
    ));

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IEventBus, RabbitMqEventBus>();
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        services.AddScoped<IOrderUnitOfWork, OrderUnitOfWork>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }

    private static IServiceCollection RegisterRabbitMqConsumers(this IServiceCollection services)
    {
        services.AddHostedService<RabbitMqEventConsumer<StockReservedEvent>>();
        services.AddHostedService<RabbitMqEventConsumer<StockFailedEvent>>();

        return services;
    }
}
