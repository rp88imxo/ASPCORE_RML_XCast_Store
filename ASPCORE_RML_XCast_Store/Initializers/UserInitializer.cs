using Microsoft.AspNetCore.Identity;
using RMLXCast.Core.Domain.Role;
using RMLXCast.Core.Domain.User;

namespace RMLXCast.Web.Initializers
{
    public class UserInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {

            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationUserRole>>();

                string adminName = "xcastadmin";
                string adminMail = "rp96imxo@gmail.com";
                string password = "awsd002W";

                string firstName = "admin";
                string lastName = "admin";

                await CreateRoleAsync(roleManager, "admin", "Админ со всеми привилегиями");
                await CreateRoleAsync(roleManager, "moderator", "Модератор контента");


                if (await userManager.FindByNameAsync(adminName) == null)
                {
                    var admin = new ApplicationUser
                    {
                        Email = adminMail,
                        UserName = adminName,
                        FirstName = firstName,
                        LastName = lastName,
                        RegistrationDateUtc = DateTime.UtcNow
                    };

                    var result = await userManager.CreateAsync(admin, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, "admin");
                    }
                }
            }
        }

        private static async Task CreateRoleAsync(RoleManager<ApplicationUserRole> roleManager, string name, string? description = null)
        {
            if (await roleManager.FindByNameAsync(name) == null)
            {
                await roleManager.CreateAsync(new ApplicationUserRole()
                {
                    Name = name,
                    Description = description
                });
            }
        }
    }
}
