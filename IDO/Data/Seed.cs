using IDO.Models;
using Microsoft.AspNetCore.Identity;

namespace IDO.Data
{
    public class Seed
    {
        public static async void SeedUsersAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                string UserEmail = "abdallahkorhani1@gmail.com";

                var user = await userManager.FindByEmailAsync(UserEmail);

                var newUser = new AppUser()
                {
                    UserName = "a",
                    Email = UserEmail,
                    EmailConfirmed = true,

                };
                await userManager.CreateAsync(newUser, "Abdallah@123");


            }
        }
    }
}
