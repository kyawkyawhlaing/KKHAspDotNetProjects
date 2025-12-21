using Domain.Users;

namespace Application.Abstractions.Authentication;

public interface ITokenProvider
{
    Task<string> CreateToken(User user);

    string GenerateRefreshToken();
}
