using Core.Context;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace UserManagementService;
public static class ApplicationConfigurator
{
    public static async Task InitializeSeedsAsync(IServiceProvider services)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            var context = services.GetRequiredService<CoreDbContext>();
            await context.Database.EnsureCreatedAsync();

            var userManager = services.GetRequiredService<UserManager<UserEntity>>();
            var configuration = services.GetRequiredService<IConfiguration>();

            string usersJson = await File.ReadAllTextAsync("Seeds/Data/Users.json");
            List<UserEntity> users = JsonConvert.DeserializeObject<List<UserEntity>>(usersJson);

            foreach (var user in users)
            {
                user.PasswordHash = new PasswordHasher<UserEntity>().HashPassword(user, configuration["AdminPassword"]);
                var result = await userManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        logger.LogError(error.Description);
                    }
                }
            }

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }
}