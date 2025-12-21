using Application.Abstractions.Authentication;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api")]
public class BaseApiController : ControllerBase
{
    protected readonly UserManager<User> _userManager;
    protected readonly ITokenProvider _tokenProvider;

    public BaseApiController(UserManager<User> userManager, ITokenProvider tokenProvider)
    {
        _userManager = userManager;
        _tokenProvider = tokenProvider;
    }

    protected async Task SetRefreshTokenCookie(User user)
    {
        string refreshToken = _tokenProvider.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = user.RefreshTokenExpiry
        };

        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }

}
