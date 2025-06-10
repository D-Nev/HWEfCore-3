using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HWEfCore_3
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; } = 0;

        public string? Description { get; set; }
        public string? TemporaryData { get; set; }
    }

    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=testDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                //1
                entity.HasKey(p => p.Id);

                //2 + 3
                entity.Property(p => p.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                //4
                entity.Property(p => p.Price)
                    .HasColumnType("decimal(10,2)");

                //5
                entity.Property(p => p.StockQuantity)
                    .HasDefaultValue(0);

                //6
                entity.Property(p => p.Description)
                    .IsRequired(false);

                //7
                entity.HasIndex(p => p.Name)
                    .IsUnique();

                //8
                entity.Ignore(p => p.TemporaryData);

                //9
                entity.ToTable("StoreProducts");
               
                //10
                entity.HasCheckConstraint("CK_Price_Positive", "Price >= 0");
            });
        }
    }
}
