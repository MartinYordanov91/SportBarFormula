using Microsoft.AspNetCore.Identity;

namespace SportBarFormula.Extensions;

/// <summary>
/// Initializes roles and default admin user.
/// </summary>
public class RoleInitializer(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    /// <summary>
    /// Initializes roles and a default admin user asynchronously.
    /// </summary>
    public async Task InitializeAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var roles = new[] { "Admin", "Manager", "Staff", "Chef", "Waiter", "Bartender", "Cleaner" };


        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var adminEmail = "Admin@gmeil.com";
        var adminPassword = "Admin123!";

        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
