using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication;

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

#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
    public Guid UserId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new UserContextUnavailableException();
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
}
