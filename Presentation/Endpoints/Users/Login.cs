
using System.Security.Claims;
using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Application.Users.Login;
using Domain.Users;
using Infrastructure.Authentication;
using Presentation.Extensions;
using Presentation.Infrastructure;

namespace Presentation.Endpoints.Users;

internal sealed class Login : IEndpoint
{
    public sealed record Reqeust(string Email, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async (
            HttpContext httpContext,
            ITokenProvider tokenProvider,
            Reqeust request,
            ICommandHandler<LoginUserCommand, User> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new LoginUserCommand(request.Email, request.Password);

            Result<User> result = await handler.Handle(command, cancellationToken);

            if (result.IsSuccess)
            {
                await httpContext.SetRefreshTokenCookie(result.Value);

                return Results.Ok(await result.Value.ToDto(tokenProvider));
            }

            return Results.InternalServerError("Authentication could not be completed due to an internal error.");
        })
        .WithTags(Tags.Users);
    }
}
