using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace JokesWebApp.Data
{
    public static class SeedData
    {

        public static async Task SeedAdminAsync(IServiceProvider services)
        {
            
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = {"Admin", "User"};

            foreach(var role in roles)
            {
                //Create Admin Role
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            
            // ensure admin user
            var addEmail = "joushal@jokes.com";
            var adminPassword = "Admin@123";

            var adminUser = await userManager.Users.FirstOrDefaultAsync(
                u => u.Email == addEmail);

            Console.WriteLine($"ADMIN USER EXIST: {adminUser != null}");
            Console.WriteLine($"ADMIN ID: {adminUser?.Id}");

            if(adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = addEmail,
                    Email = addEmail,
                    EmailConfirmed = true
                };

                var result =await userManager.CreateAsync(adminUser, adminPassword);

                if(!result.Succeeded)
                {
                    throw new Exception("Failed to create admin user: " +
                    string.Join(", ", result.Errors.Select(e => e.Description))
                    );
                }
            }
            // Assign admin user to Admin role
            if(!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
