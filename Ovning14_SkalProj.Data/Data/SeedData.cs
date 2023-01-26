using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using Ovning14_SkalProj.Core.Entities;

namespace Ovning14_SkalProj.Data.Data
{
    public class SeedData
    {
        private static Faker faker = null!;
        public static async Task InitAsync(ApplicationDbContext db)
        {
            //Guard Clause if something 
            if (await db.instructors.AnyAsync()) return;

            faker = new Faker("fr");

            var intructors = GenerateInstructors(50);
            await db.AddRangeAsync(intructors);
            await db.SaveChangesAsync();

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
