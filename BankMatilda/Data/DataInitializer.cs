using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankMatilda.Data
{
    public class DataInitializer
    {
        public static void SeedData(BankAppDataContext dbContext, UserManager<IdentityUser> userManager)
        {
            dbContext.Database.Migrate();

            SeedRoles(dbContext);
            SeedUsers(userManager);
            SeedPersoner(dbContext);
        }


        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            AddUserIfNotExists(userManager, "stefan.holmberg@systementor.se", "Hejsan123#", new string[] { "Admin" });
            AddUserIfNotExists(userManager, "stefan.holmberg@nackademin.se", "Hejsan123#", new string[] { "Cashier" });
        }

        private static void AddUserIfNotExists(UserManager<IdentityUser> userManager,
            string userName, string password, string[] roles)
        {
            if (userManager.FindByEmailAsync(userName).Result != null) return;

            var user = new IdentityUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };
            var result = userManager.CreateAsync(user, password).Result;
            var r = userManager.AddToRolesAsync(user, roles).Result;
        }

        private static void SeedRoles(BankAppDataContext dbContext)
        {
            var role = dbContext.Roles.FirstOrDefault(r => r.Name == "Admin");
            if (role == null)
            {
                dbContext.Roles.Add(new IdentityRole { Name = "Admin", NormalizedName = "Admin" });
            }
            role = dbContext.Roles.FirstOrDefault(r => r.Name == "Cashier");
            if (role == null)
            {
                dbContext.Roles.Add(new IdentityRole { Name = "Cashier", NormalizedName = "Cashier" });
            }
            dbContext.SaveChanges();

        }

        private static void SeedPersoner(BankAppDataContext dbContext)
        {
            var person = dbContext.Users.FirstOrDefault(r => r.FirstName == "Nichole Tsykhotsky");
            if (person == null)
                dbContext.Users.Add(new User
                {
                    FirstName = "Nichole",
                    LoginName = "nichole,tsykhotsky@gmail.com",
                    LastName = "Tsykhotsky",
                    PasswordHash = new byte[24]
                });

            dbContext.SaveChanges();
        }
        //TODO: Add hashpawssword ex https://gist.github.com/hclewk/a41d937eba12c0388f70429a997cd7ec
    }
}
