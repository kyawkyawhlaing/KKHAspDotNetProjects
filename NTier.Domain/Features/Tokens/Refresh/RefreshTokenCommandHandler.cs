
using NTier.Domain.Abstractions.Authentications;

namespace NTier.Domain.Features.Tokens.Refresh;

internal sealed class RefreshTokenCommandHandler(ITokenProvider tokenProvider) : ICommandHandler<RefreshTokenCommand, TokenResponseDto>
{
    public async Task<Result<TokenResponseDto>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        User user = await tokenProvider.ValidateRefreshTokenAsync(command.UserId, command.RefreshToken);

        if (user is null)
        {
            return Result.Failure<TokenResponseDto>(TokenErrors.InvalidRefreshToken);
        }

        TokenResponseDto token = await tokenProvider.CreateTokenResponse(user!);

        if (token is null)
        {
            return Result.Failure<TokenResponseDto>(TokenErrors.InvalidRefreshToken);
        }

        return token;
    }
}
