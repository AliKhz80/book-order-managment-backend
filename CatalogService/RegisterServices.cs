using CatalogService.Application;
using CatalogService.Infrastructure;

namespace CatalogService;

public static class RegisterServices
{
    public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .ConfigureInfrustructorLayer(configuration)
            .ConfigureApplicationLayer()
            .RegisterMiddlewares();

        return services;
    }

    private static IServiceCollection RegisterMiddlewares(this IServiceCollection services)
    {
        return services;
    }
}
