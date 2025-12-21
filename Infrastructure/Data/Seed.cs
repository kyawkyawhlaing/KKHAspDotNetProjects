using Microsoft.AspNetCore.Identity;

namespace MyProject.Infrastructure.Data;

public sealed class Seed
{
    public static async Task SeedAdminUser(UserManager<User> userManager)
    {
        var admin = new User
        {
            UserName = "admin@test.com",
            Email = "admin@test.com",
            DisplayName = "Admin"
        };
        await userManager.CreateAsync(admin, "Pa$$w0rd");
        await userManager.AddToRolesAsync(admin, ["Admin", "Moderator"]);
    }
}
