using IdentityService.Domain.Interfaces;
using IdentityService.Domain.Interfaces.BusinessIRepositories;
using IdentityService.Infrastructure.BusinessRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure.DI;
public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrustructorLayer(this IServiceCollection services , IConfiguration configuration)
    {
        services
            .RegisterDBContext(configuration)
            .RegisterRepositories();

        return services;
    }

    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        return services;
    }

    private static IServiceCollection RegisterDBContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
        options.UseSqlServer(
            configuration.GetConnectionString("BookOrderManagementIdentityDB"),
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                );
            }
        ));


        return services;
    }
}
