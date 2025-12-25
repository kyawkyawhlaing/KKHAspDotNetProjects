
using Application.Abstractions.Messaging;
using Application.Users.Login;
using Presentation.Extensions;
using Presentation.Infrastructure;

namespace Presentation.Endpoints.Users;

internal sealed class Login : IEndpoint
{
    public sealed record Reqeust(string Email, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async (
            Reqeust request,
            ICommandHandler<LoginUserCommand, string> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new LoginUserCommand(request.Email, request.Password);

            Result<string> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
