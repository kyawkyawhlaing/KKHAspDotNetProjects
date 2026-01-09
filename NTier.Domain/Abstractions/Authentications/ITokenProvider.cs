using System.Security.Cryptography;
using NTier.Database.Entites;

namespace NTier.Domain.Abstractions.Authentications;

public interface ITokenProvider
{
    Task<TokenResponseDto> CreateTokenResponse(User user);

    Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken);
}
