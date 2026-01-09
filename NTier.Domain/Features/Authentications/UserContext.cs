using Microsoft.AspNetCore.Http;
using NTier.Domain.Abstractions.Authentications;

namespace NTier.Domain.Features.Authentications;

public sealed class UserContext : IUserContext
{
    public sealed class UserContextUnavailableException : Exception
    {
        public UserContextUnavailableException() : base("User context is unavailable")
        {
        }
    }

    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1065:Do not raise exceptions in unexpected locations", Justification = "<Pending>")]
    public Guid UserId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new UserContextUnavailableException();
}
