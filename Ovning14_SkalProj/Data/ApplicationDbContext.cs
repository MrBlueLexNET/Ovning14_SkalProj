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
        }
        public DbSet<Ovning14_SkalProj.Models.GymClass> Course { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FluentAPI goes here
            base.OnModelCreating(modelBuilder);


            // Defined composite key for junction entity (kopplingstabell)
            modelBuilder.Entity<ApplicationUserGymClass>()
                .HasKey(e => new { e.GymClassId, e.ApplicationUserId });

            // Shadow Property
            //modelBuilder.Entity<Student>().Property<DateTime>("Edited");
        }

    }
}