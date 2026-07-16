using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MediatR;
using Auth;

namespace IdentityService.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .RegisterMediatR()
                .RegisterJwt(configuration);

            return services;
        }

        private static IServiceCollection RegisterMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            return services;
        }

        private static IServiceCollection RegisterJwt(this IServiceCollection services, IConfiguration configuration)
        {
            // Register standard JWT token generation services from Auth
            services.AddJwt(configuration);
            return services;
        }
    }
}
