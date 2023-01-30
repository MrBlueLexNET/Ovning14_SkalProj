using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ovning14_SkalProj.Core.Entities;
using Ovning14_SkalProj.Models;

namespace Ovning14_SkalProj.Data.Data
{
    public class SeedData
    {
        private static Faker faker = null!;
        private static ApplicationDbContext db = default!;
        private static RoleManager<IdentityRole> roleManager = default!;
        private static UserManager<ApplicationUser> userManager = default!;
        public static async Task InitAsync(ApplicationDbContext context, IServiceProvider services, string adminPW)
        {
            

            faker = new Faker("fr");

            ArgumentNullException.ThrowIfNull(nameof(context));
            db = context;

            //Guard Clause if something 
            if (await db.instructors.AnyAsync()) return;

            if (db.GymClasses.Any()) return;
            ArgumentNullException.ThrowIfNull(nameof(services));
            ArgumentNullException.ThrowIfNull(adminPW, nameof(adminPW));

            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            ArgumentNullException.ThrowIfNull(roleManager);

            userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            ArgumentNullException.ThrowIfNull(userManager);

            var roleNames = new[] { "Member", "Admin" };
            var adminEmail = "admin@gym.se";

            await AddRolesAsync(roleNames);

            var admin = await AddAdminAsync(adminEmail, adminPW);

            await AddToRolesAsync(admin, roleNames);

            var intructors = GenerateInstructors(50);
            await db.AddRangeAsync(intructors);
            await db.SaveChangesAsync();

        }

        private static async Task AddToRolesAsync(ApplicationUser admin, string[] roleNames)
        {
            foreach (var role in roleNames)
            {
                if (await userManager.IsInRoleAsync(admin, role)) continue;
                var result = await userManager.AddToRoleAsync(admin, role);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        private static async Task<ApplicationUser> AddAdminAsync(string adminEmail, string adminPW)
        {
            var found = await userManager.FindByEmailAsync(adminEmail);

            if (found != null) return null!;

            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail
            };

            var result = await userManager.CreateAsync(admin, adminPW);
            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            return admin;
        }

        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        private static IEnumerable<Instructor> GenerateInstructors(int numberOfInstructor)
        {
            var instructors = new List<Instructor>();
            for (int i = 0; i < numberOfInstructor; i++) 
            {
                var fName = faker.Name.FirstName();
                var lName = faker.Name.LastName();
                var isPersonalTrainer = faker.Random.Bool();
                var biography = faker.Lorem.Paragraph();
                //var rowVersion = faker.Date.Timespan();

                var instructor = new Instructor
                {
                    FirstName = fName,
                    LastName = lName,
                    IsPersonalTrainer = isPersonalTrainer,
                    Biography = biography
                    //RowVersion = rowVersion

                };
                instructors.Add(instructor);

            }
            return instructors;
            
        }
    }
}
