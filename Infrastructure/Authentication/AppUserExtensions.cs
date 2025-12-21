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
}
