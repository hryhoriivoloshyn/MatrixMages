using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MatrixMages.Models;

#nullable disable

namespace MatrixMages.Data
{
    public partial class MatrixMagesdbContext : DbContext
    {
        public MatrixMagesdbContext()
        {
        }

        public MatrixMagesdbContext(DbContextOptions<MatrixMagesdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BattleMageStrategy> BattleMageStrategies { get; set; }
        public virtual DbSet<SpaceMageStrategy> SpaceMageStrategies { get; set; }
        public virtual DbSet<Victory> Victories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-AB6K1OD;Initial Catalog=MatrixMagesdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<BattleMageStrategy>(entity =>
            {
                entity.Property(e => e.StrategyName).HasMaxLength(50);
            });

            modelBuilder.Entity<SpaceMageStrategy>(entity =>
            {
                entity.Property(e => e.StrategyName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Victory>(entity =>
            {
                entity.HasKey(e => new { e.SpaceMageId, e.BattleMageId });

                entity.Property(e => e.Victory1).HasColumnName("Victory");

                entity.HasOne(d => d.BattleMage)
                    .WithMany(p => p.Victories)
                    .HasForeignKey(d => d.BattleMageId)
                    .HasConstraintName("FK_Victories_BattleMageStrategies");

                entity.HasOne(d => d.SpaceMage)
                    .WithMany(p => p.Victories)
                    .HasForeignKey(d => d.SpaceMageId)
                    .HasConstraintName("FK_Victories_SpaceMageStrategies");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
