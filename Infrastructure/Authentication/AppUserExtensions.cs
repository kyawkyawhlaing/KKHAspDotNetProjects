using System.Security.Cryptography;
using Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authentication;

public static class AppUserExtensions
{
    public static async Task<UserDto> ToDto(this User user, ITokenProvider tokenProvider)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            DisplayName = user.DisplayName,
            Token = await tokenProvider.CreateToken(user)
        };
    }

    public static async Task SetRefreshTokenCookie(this HttpContext httpContext, Guid id)
    {
        UserManager<User> userManager = httpContext.RequestServices.GetRequiredService<UserManager<User>>();

        string refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        User user = await userManager.Users.Where(x => x.Id == id).SingleOrDefaultAsync() ?? throw new ApplicationException("User not found");

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        await userManager.UpdateAsync(user);

        httpContext.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = user.RefreshTokenExpiry
        });
    }

}
