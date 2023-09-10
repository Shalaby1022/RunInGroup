
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RunInGroup.Models;

namespace RunInGroup.Data
{

    public class RunDbContext : IdentityDbContext<AppUser>
    {


       public DbSet<Race> Races { get; set; } 
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresss { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        public RunDbContext(DbContextOptions<RunDbContext> options ): base(options)
        {
        }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the primary key for IdentityUserLogin
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey });

        }
    }
}
