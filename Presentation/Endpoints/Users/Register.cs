
using System.Threading;
using Application.Abstractions.Messaging;
using MyProject.Application.Users.Register;
using Presentation.Extensions;
using Presentation.Infrastructure;

namespace Presentation.Endpoints.Users;

internal sealed class Register : IEndpoint
{
    public sealed record Request(
            string Email,
            string DisplayName,
            string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (
            Request request,
            ICommandHandler<RegisterUserCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new RegisterUserCommand(
                request.Email,
                request.DisplayName,
                request.Password);

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
