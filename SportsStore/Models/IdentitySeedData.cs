using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace SportsStore.Models
{
    public class IdentitySeedData
    {
        private const string AdminUser = "Admin";
        private const string adminPassword ="Secret123$";
        public static async void EnsurePopulated(IApplicationBuilder app){
                AppIdentityDbContext ctx = app.ApplicationServices.CreateScope()
                                           .ServiceProvider.GetRequiredService<AppIdentityDbContext>();
            if(ctx.Database.GetPendingMigrations().Any())
            {
                ctx.Database.Migrate();
            }
            UserManager<IdentityUser> userManager = app.ApplicationServices.CreateScope()
                                                    .ServiceProvider
                                                    .GetRequiredService<UserManager<IdentityUser>>();
            IdentityUser user = await userManager.FindByIdAsync(AdminUser);
            if(user == null)
            {
               await  userManager.CreateAsync(new IdentityUser("Admin"){
                    Email="admin@exampe.com",
                    PhoneNumber = "555-1234"
                 },adminPassword);
                
            }

        }
    }
}