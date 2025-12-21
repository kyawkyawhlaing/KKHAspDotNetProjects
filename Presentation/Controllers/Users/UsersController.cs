using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Domain.Users;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Application.Users.Register;

namespace Presentation.Controllers.Users;


public class UsersController(UserManager<User> userManager, ITokenProvider tokenProvider)
    : BaseApiController(userManager, tokenProvider)
{

    [HttpPost("users/register")]
    public async Task<ActionResult> UserLogin(
        RegisterUserRequest request,
        ICommandHandler<RegisterUserCommand, Guid> handler,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.DisplayName,
            request.Password);

        Result<Guid> result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        var user = new User { Id = result.Value, Email = request.Email, DisplayName = request.DisplayName, PasswordHash = request.Password };

        await SetRefreshTokenCookie(user);

        return Ok(user.ToDto(_tokenProvider));
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _userManager.Users
            .Where(x => x.Id == User.GetUserId())
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.RefreshToken, _ => null)
                .SetProperty(x => x.RefreshTokenExpiry, _ => null)
            );

        Response.Cookies.Delete("refreshToken");

        return Ok();

    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<UserDto>> RefreshToken()
    {
        string refreshToken = Request.Cookies["refreshToken"];

        if (refreshToken is null)
        {
            return NoContent();
        }

        User user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken
                && x.RefreshTokenExpiry > DateTime.UtcNow);

        if (user is null)
        {
            return Unauthorized();
        }

        await SetRefreshTokenCookie(user);

        return await user.ToDto(_tokenProvider);
    }

}
