using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ovning14_SkalProj.Data;
using Ovning14_SkalProj.Data.Data;
using Ovning14_SkalProj.Extensions;
using Ovning14_SkalProj.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

await app.SeedDataAsync();

//Open service for accessing the Db SeedData
////using (var scope = app.Services.CreateScope())
////{
////    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

////    //Drop Db
////    //db.Database.EnsureDeleted();
////    //Update Db
////    //db.Database.Migrate();

////    try
////    {
////        await SeedData.InitAsync(db);
////    }
////    catch (Exception e)
////    {
////        Console.WriteLine(e.Message);
////        throw;
////    }
////}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
