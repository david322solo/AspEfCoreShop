using EFDataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataLibrary.DataAccess
{
    public class UndefinedContext : DbContext
    {
        public UndefinedContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductPhoto>()
                .HasKey(t => new { t.ProductId, t.PhotoId });

            modelBuilder.Entity<ProductPhoto>()
                .HasOne(pr => pr.Product)
                .WithMany(ph => ph.ProductPhotos)
                .HasForeignKey(sc => sc.ProductId);

           
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<ProductPhoto> ProductPhotos {get;set;}
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}
