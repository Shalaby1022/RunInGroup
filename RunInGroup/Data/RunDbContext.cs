using Microsoft.EntityFrameworkCore;
using RunInGroup.Data.Enums;
using RunInGroup.Models;

namespace RunInGroup.Data
{
    public class RunDbContext : DbContext
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
            
        }
    }
}
