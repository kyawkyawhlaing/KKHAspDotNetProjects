namespace NTier.Domain.Features.Tokens;

internal sealed class TokenErrors
{
    public static Error InvalidRefreshToken => Error.Failure(
        "Tokens.InvalidRefreshToken",
        $"Token is expired or invalid due to some reasons.");
}
