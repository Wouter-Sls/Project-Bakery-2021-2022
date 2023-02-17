using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ProjectBakery.Domain;

namespace ProjectBakery.DAL.EF
{
    public class ProjectBakeryDbContext : DbContext
    {
        
        public DbSet<Baker> Bakers { get; set; }

        public DbSet<Bakery> Bakeries { get; set; }

        public DbSet<Pastrie> Pastries { get; set; }
        
        public DbSet<StockProduct> StockProducts { get; set; }

        public ProjectBakeryDbContext()
        {
            ProjectBakeryInitializer.Initialize(this, true);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=../../../../bakery.db")
                    .LogTo(message => Debug.WriteLine(message));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<StockProduct>().HasOne(s => s.Pastrie).WithMany(p => p.StockProducts).HasForeignKey("FKPastryId");
            modelbuilder.Entity<StockProduct>().HasOne(s => s.Bakerie).WithMany(b => b.StockProducts).HasForeignKey("FKBakeryId");
            modelbuilder.Entity<Bakery>().HasOne(b => b.Baker).WithOne(b => b.Bakery).HasForeignKey<Baker>("FKBakerId");
            modelbuilder.Entity<Bakery>().HasKey(b => b.Id);
            modelbuilder.Entity<Pastrie>().HasKey(p => p.Id);
            modelbuilder.Entity<Baker>().HasKey(b => b.Id);
            modelbuilder.Entity<StockProduct>().HasKey(s => s.Id);


        }
    }
}