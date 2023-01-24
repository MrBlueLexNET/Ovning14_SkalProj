using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ovning14_SkalProj.Models;

namespace Ovning14_SkalProj.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<GymClass> GymClasses => Set<GymClass>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FluentAPI goes here
            base.OnModelCreating(modelBuilder);


            // Defined composite key for junction entity (kopplingstabell)
            modelBuilder.Entity<ApplicationUserGymClass>()
                .HasKey(e => new { e.ApplicationUserId, e.GymClassId });

            // Shadow Property
            //modelBuilder.Entity<Student>().Property<DateTime>("Edited");
        }

    }
}