using System;
using System.Collections.Generic;
using System.Text;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.RefreshToken;

internal sealed class RefreshTokenQueryHandler(UserManager<User> userManager) : IQueryHandler<RefreshTokenQuery, User>
{
    public async Task<Result<User>> Handle(RefreshTokenQuery query, CancellationToken cancellationToken = default)
    {
        User user = await userManager.Users
            .FirstOrDefaultAsync(
                x => x.RefreshToken == query.RefreshToken &&
                     x.RefreshTokenExpiry > DateTime.UtcNow, cancellationToken);

        if (user is null)
        {
            return Result.Failure<User>(UserErrors.TokenNotFound());
        }

        return user;
    }
}
