using Ovning14_SkalProj.Data.Data;
using Ovning14_SkalProj.Data;
using Microsoft.EntityFrameworkCore;

namespace Ovning14_SkalProj.Extensions
{
    public static class ApplicationBuilderExtensions
    {

        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

            //db.Database.EnsureDeleted();
            //db.Database.Migrate();

            //dotnet user-secrets set "AdminPW" "ChangeMe123!"
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            var adminPW = config["AdminPW"];

            ArgumentNullException.ThrowIfNull(adminPW, nameof(adminPW));

            try
            {
                await SeedData.InitAsync(db, serviceProvider, adminPW);
                //await SeedData.InitAsync(db);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}

