using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ASPNET_CoreSessionApps.Models
{
    public partial class SuperMarketContext : DbContext
    {
        

        public SuperMarketContext(DbContextOptions<SuperMarketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BillDetails> BillDetails { get; set; }
        public virtual DbSet<BillMaster> BillMaster { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Product> Product { get; set; }
 

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SuperMarket;Integrated Security=SSPI");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillDetails>(entity =>
            {
                entity.HasKey(e => e.BillItemId)
                    .HasName("PK__BillDeta__47605AF5B1303B65");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.BillNoNavigation)
                    .WithMany(p => p.BillDetails)
                    .HasForeignKey(d => d.BillNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BillDetai__BillN__2B3F6F97");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.BillDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BillDetai__Produ__2A4B4B5E");
            });

            modelBuilder.Entity<BillMaster>(entity =>
            {
                entity.HasKey(e => e.BillNo)
                    .HasName("PK__BillMast__11F28419A9C2EE03");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Categor__25869641");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
