using Microsoft.EntityFrameworkCore;
using Dinotrack.Shared.Entities;

namespace Dinotrack.Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Country> Countries { get; set; }   

        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; } 

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Reference> References { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Reference>().HasIndex(r => new { r.BrandId, r.Name }).IsUnique();
            modelBuilder.Entity<Brand>().HasIndex(b => b.Name).IsUnique();
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex(s => new { s.CountryId, s.Name }).IsUnique();
            modelBuilder.Entity<City>().HasIndex(c => new { c.StateId, c.Name }).IsUnique();
        }




    }   
}
