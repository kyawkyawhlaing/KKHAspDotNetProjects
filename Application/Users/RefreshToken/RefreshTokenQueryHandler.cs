using System;
using System.Collections.Generic;
using System.Text;
using Application.Abstractions.Authentication;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.RefreshToken;

internal sealed class RefreshTokenQueryHandler(
    UserManager<User> userManager,
    ITokenProvider tokenProvider) : IQueryHandler<RefreshTokenQuery, UserDto>
{
    public async Task<Result<UserDto>> Handle(RefreshTokenQuery query, CancellationToken cancellationToken = default)
    {
        User user = await userManager.Users
            .FirstOrDefaultAsync(
                x => x.RefreshToken == query.RefreshToken &&
                     x.RefreshTokenExpiry > DateTime.UtcNow, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDto>(UserErrors.InvalidRefreshToken());
        }

        var userDto = new UserDto
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email!,
            Token = await tokenProvider.CreateToken(user)
        };

        return userDto;
    }
}
