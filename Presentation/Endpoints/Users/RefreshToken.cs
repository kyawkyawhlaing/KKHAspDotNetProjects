
using System.Threading;
using Application.Abstractions.Authentication;
using Application.Abstractions.Messaging;
using Application.Users.RefreshToken;
using Domain.Users;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Presentation.Extensions;
using static Presentation.Endpoints.Users.Register;

namespace Presentation.Endpoints.Users;

internal sealed class RefreshToken : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/refresh-token", async (
            HttpContext httpContext,
            ITokenProvider tokenProvider,
            IQueryHandler<RefreshTokenQuery, User> handler,
            CancellationToken cancellationToken) =>
        {
            string refreshToken = httpContext.Request.Cookies["refreshToken"]!;

            var query = new RefreshTokenQuery(refreshToken);

            Result<User> result = await handler.Handle(query, cancellationToken);

            if (result.IsSuccess)
            {
                await httpContext.SetRefreshTokenCookie(result.Value);

                return Results.Ok(await result.Value.ToDto(tokenProvider));
            }

            return Results.InternalServerError("Unable to generate refresh token. Please try again later.");
        })
        .WithTags(Tags.Users);
        //.HasPermission();
    }
}
