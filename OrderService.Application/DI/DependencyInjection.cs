using Microsoft.Extensions.DependencyInjection;

namespace OrderService.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            cfg.AddOpenBehavior(typeof(BuildingBlocks.Behaviors.LoggingBehavior<,>));
        });

        return services;
    }
}
