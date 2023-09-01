using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using RapidPayAPI.Constants;
using RapidPayAPI.Domain;

namespace RapidPayAPI.Data
{
    public interface IDatabaseSeeder
    {
        void SeedData();
    }

    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;

        public DatabaseSeeder(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            DataContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void SeedData()
        {
            //cards

            _context.Cards.Add(new Card
            {
                Id = 1,
                Number = "123456789012345",
                CardHolderName = "John Doe",
                ExpirationMonth = 12,
                ExpirationtYear = 2024,
                CVC = "123"
            });

            _context.Cards.Add(new Card
            {
                Id = 2,
                Number = "987654321098765",
                CardHolderName = "Homer Simpson",
                ExpirationMonth = 6,
                ExpirationtYear = 2025,
                CVC = "321"
            });

            //Admin
            Task.Run(async () =>
            {
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }
                IdentityUser admin = new()
                {
                    Email = "admin@company.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin"
                };
                var result = await _userManager.CreateAsync(admin, "Pass.123");
                await _userManager.AddToRoleAsync(admin, UserRoles.Admin);
            }).GetAwaiter().GetResult();

            //user
            Task.Run(async () =>
            {
                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }
                IdentityUser user = new()
                {
                    Email = "user@company.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "user"
                };
                var result = await _userManager.CreateAsync(user, "Pass.123");
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }).GetAwaiter().GetResult();

            _context.SaveChanges();
        }
    }
}
