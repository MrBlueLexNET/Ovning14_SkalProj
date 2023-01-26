using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;

namespace Ovning14_SkalProj.Data.Data
{
    public class SeedData
    {
        private static Faker faker = null!;
        public static async Task InitAsync(ApplicationDbContext db)
        {
            //Guard Clause if something 
            if (await db.instructors.AnyAsync()) return;

            faker = new Faker("sv");

            var intructors = GenerateInstructors(50);
            await db.AddRangeAsync(intructors);

        }
    }
}
