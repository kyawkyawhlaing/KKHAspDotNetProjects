
using System.Threading;
using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Application.Users.RefreshToken;
using Domain.Users;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Presentation.Extensions;
using Presentation.Infrastructure;
using static Presentation.Endpoints.Users.Register;

namespace Presentation.Endpoints.Users;

internal sealed class RefreshToken : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/refresh-token", async (
            HttpContext httpContext,
            ITokenProvider tokenProvider,
            IQueryHandler<RefreshTokenQuery, UserDto> handler,
            CancellationToken cancellationToken) =>
        {
            string refreshToken = httpContext.Request.Cookies["refreshToken"]!;

            var query = new RefreshTokenQuery(refreshToken);

            Result<UserDto> result = await handler.Handle(query, cancellationToken);

            if (result.IsSuccess)
            {
                await httpContext.SetRefreshTokenCookie(result.Value.Id);
            }

            return result.Match(Results.Ok, CustomResults.Problem);

        })
        .HasPermission()
        .WithTags(Tags.Users);
    }
}
