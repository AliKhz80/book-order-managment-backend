using OrderService.Application;
using OrderService.Infrastructure;

namespace OrderService;

public static class RegisterServices
{
    public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .ConfigureInfrastructureLayer(configuration)
            .ConfigureApplicationLayer();

        return services;
    }
}
