using Domain.Users;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyProject.Infrastructure.Data;

namespace Presentation.Extensions;

public static class MigrationExtensions
{
    public static async Task ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ApplicationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        await dbContext.Database.MigrateAsync();
        await Seed.SeedAdminUser(userManager);
    }
}
