using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.NET06.Parfume.Manager.MVC.Data.Models;

namespace TMS.NET06.Parfume.Manager.MVC.Data
{
    public class ParfumeShopContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public ParfumeShopContext (DbContextOptions<ParfumeShopContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Brand>(new Action<Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Brand>>(BuildBrand));
            modelBuilder.Entity<Brand>(brandBuilder =>
            {
                brandBuilder.HasKey(b => b.BrandId);
            });

            modelBuilder.Entity<Product>()
               .HasOne(p => p.Brand)
               .WithMany(b => b.Products)
               .HasForeignKey("BrandId")
               .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Product>()
           .Property(p => p.Gender)
           .IsRequired(false);

            modelBuilder.Entity<Product>()
          .Property(p => p.ImageId)
          .IsRequired(false);
        }

        //private void BuildBrand(EntityTypeBuilder<Brand> parameterName)
        //{
        //    throw new NotImplementedException();
        //}

        
    }
}
