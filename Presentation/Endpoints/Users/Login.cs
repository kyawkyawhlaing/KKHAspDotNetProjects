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
            ICommandHandler<LoginUserCommand, UserDto> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new LoginUserCommand(request.Email, request.Password);

            Result<UserDto> result = await handler.Handle(command, cancellationToken);

            if (result.IsSuccess)
            {
                await httpContext.SetRefreshTokenCookie(result.Value.Id);
            }

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
