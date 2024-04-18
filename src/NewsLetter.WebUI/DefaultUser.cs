using Microsoft.AspNetCore.Identity;
using NewsLetter.Domain.Entities;

namespace NewsLetter.WebUI;

public static class DefaultUser
{
    public static async Task CreateDefaultUserAsync(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            if (!userManager.Users.Any())
            {
                //default user information
                AppUser? user = new()
                {
                    Email = "admin@admin.com",
                    UserName = "admin"
                };

                await userManager.CreateAsync(user, "String1*");
            }
        }
    }
}
