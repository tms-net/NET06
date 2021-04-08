using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TMS.NET06.Lesson26.EFScaffolding
{
    public partial class TMSContext : DbContext
    {
        public TMSContext()
        {
        }

        public TMSContext(DbContextOptions<TMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HomeWork> HomeWorks { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLExpress;Initial Catalog=TMS;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<HomeWork>(entity =>
            {
                entity.Property(e => e.Mark)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.PullRequestUrl).HasMaxLength(250);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.HomeWorks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HomeWorks_ToTable");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.BirthDay).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
