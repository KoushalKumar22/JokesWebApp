using Microsoft.AspNetCore.Identity;

namespace JokesWebApp.Data
{
    public static class SeedData
    {

        public static async Task SeedAdminAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            //Create Admin Role
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            //Assign admin role to a user
            var adminUser = await userManager.FindByEmailAsync("joushal@jokes.com");
            if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
