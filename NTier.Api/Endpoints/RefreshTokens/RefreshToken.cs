
using NTier.Domain.Features.Tokens.Refresh;

namespace NTier.Api.Endpoints.RefreshTokens;

internal sealed class RefreshToken : IEndpoint
{
    public sealed record RefreshTokenRequestDto(Guid UserId, string RefreshToken);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("refresh-tokens", async (
            RefreshTokenRequestDto request,
            ICommandHandler<RefreshTokenCommand, TokenResponseDto> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new RefreshTokenCommand(request.UserId, request.RefreshToken);

            Result<TokenResponseDto> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        });
    }
}
