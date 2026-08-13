using BuildingBlocks.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.Interfaces;
using OrderService.Domain.Interfaces.Repositories.BusinessIRepositories.OrderRepository;
using OrderService.Infrastructure.Repositories.BusinessRepositories.OrderRepository;

namespace OrderService.Infrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .RegisterDbContext(configuration)
            .RegisterRepositories()
            .AddMassTransitWithRabbitMq(configuration, typeof(Application.DI.DependencyInjection).Assembly);

        return services;
    }

    public static IServiceCollection ConfigureInfrustructorLayer(
        this IServiceCollection services,
        IConfiguration configuration) => ConfigureInfrastructureLayer(services, configuration);

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
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderRepositoryQuery, OrderRepositoryQuery>();
        services.AddScoped<IOrderRepositoryCommond, OrderRepositoryCommond>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
