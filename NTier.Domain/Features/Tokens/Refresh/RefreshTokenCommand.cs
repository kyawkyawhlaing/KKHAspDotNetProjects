namespace NTier.Domain.Features.Tokens.Refresh;

public sealed record RefreshTokenCommand(Guid UserId, string RefreshToken) : ICommand<TokenResponseDto>;
