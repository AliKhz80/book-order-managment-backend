using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public static class JwtDependencyInjection
    {
        /// <summary>
        /// Registers the JWT Generator Service (Used by the Identity Service to ISSUE tokens).
        /// </summary>
        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            return services;
        }

        /// <summary>
        /// Configures JWT Bearer authentication validation (Used by Gateway or Microservices to VALIDATE tokens).
        /// </summary>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

            if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.SecretKey))
            {
                throw new InvalidOperationException("JWT settings are not configured properly. Ensure 'JwtSettings' exists in your configuration.");
            }

            var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization();

            return services;
        }
    }
}