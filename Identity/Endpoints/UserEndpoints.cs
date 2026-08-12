using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MediatR;
using BuildingBlocks.Behaviors;
using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityService.Application.UseCases.User.Commonds.RegisterUserCommand;
using IdentityService.Application.UseCases.User.Queries.LoginUserQuery;
using Asp.Versioning.Builder;
using Asp.Versioning;

namespace Identity.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var apiVersionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1,0))
                .ReportApiVersions()
                .Build();

            var group = app.MapGroup("api/v{version:apiVersion}/user")
                           .WithApiVersionSet(apiVersionSet)
                           .WithTags("User");

            group.MapPost("register", RegisterAsync)
                 .MapToApiVersion(1,0)
                 .WithName("Register")
                 .Produces<IdentityService.Application.UseCases.User.ViewModels.UserResponseDto>(StatusCodes.Status200OK)
                 .Produces(StatusCodes.Status400BadRequest)
                 .Produces(StatusCodes.Status499ClientClosedRequest);

            group.MapPost("login", LoginAsync)
                 .MapToApiVersion(1,0)
                 .WithName("Login")
                 .Produces<IdentityService.Application.UseCases.User.ViewModels.UserResponseDto>(StatusCodes.Status200OK)
                 .Produces(StatusCodes.Status400BadRequest)
                 .Produces(StatusCodes.Status401Unauthorized);

            return app;
        }

        private static async Task<IResult> RegisterAsync(
            RegisterUserCommandRequest command,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            // Validate using DataAnnotations
            var (isValid, errors) = command.Validate();
            if (!isValid)
            {
                return Results.BadRequest(new { errors });
            }

            try
            {
                var response = await mediator.Send(command, cancellationToken);
                return Results.Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return Results.Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        }

        private static async Task<IResult> LoginAsync(
            LoginUserQueryRequest query,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            // Validate using DataAnnotations
            var (isValid, errors) = query.Validate();
            if (!isValid)
            {
                return Results.BadRequest(new { errors });
            }

            try
            {
                var response = await mediator.Send(query, cancellationToken);
                return Results.Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Results.Json(new { error = ex.Message }, statusCode: StatusCodes.Status401Unauthorized);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        }
    }
}
