
using System.Security.Claims;
using Application.Abstractions.Messaging;
using Application.Users.Logout;
using Domain.Users;
using Infrastructure.Authentication;
using Presentation.Extensions;
using Presentation.Infrastructure;

namespace Presentation.Endpoints.Users;

internal sealed class Logout : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/logout", async (
            HttpContext httpContext,
            ICommandHandler<LogoutUserCommand, string> handler,
            CancellationToken cancellationToken) => 
        {
            Guid userId = httpContext.User.GetUserId();

            var command = new LogoutUserCommand(userId!);

            Result<string> result = await handler.Handle(command, cancellationToken);

            if (result.IsSuccess)
            {
                httpContext.Response.Cookies.Delete("refreshToken");
            }

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .HasPermission()
        .WithTags(Tags.Users);
    }
}
