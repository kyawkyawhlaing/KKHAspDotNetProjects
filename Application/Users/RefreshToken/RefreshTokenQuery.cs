using Domain.Users;

namespace Application.Users.RefreshToken;

public sealed record RefreshTokenQuery(string RefreshToken) : IQuery<User>;
