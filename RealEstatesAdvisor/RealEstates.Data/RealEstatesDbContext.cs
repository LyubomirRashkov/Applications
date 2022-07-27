using Microsoft.EntityFrameworkCore;
using RealEstates.Models;

namespace RealEstates.Data
{
    public class RealEstatesDbContext : DbContext
    {
        public RealEstatesDbContext()
        {
        }

        public RealEstatesDbContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<BuildingType> BuildingTypes { get; set; }

        public DbSet<PropertyTag> PropertiesTags { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = (local)\\SQLEXPRESS; Database = RealEstates; Integrated Security = true; Encrypt = False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PropertyTag>().HasKey(pt => new { pt.PropertyId, pt.TagId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
