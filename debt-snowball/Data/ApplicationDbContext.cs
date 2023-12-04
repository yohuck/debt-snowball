using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace debt_snowball.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        public DbSet<Payment> Payments { get; set; }

        public DbSet<Debt> Debts { get; set; }
        public DbSet<Snowballa> Snowballas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany<Debt>(a => a.Debts)
                .WithOne(d => d.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Snowballa>()
                .HasKey(s => s.SnowballId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany<Snowballa>(a => a.Snowballas)
                .WithOne(s => s.User)
                .OnDelete(DeleteBehavior.Cascade);

        }




    }
}
