using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MutantDetector.Core.Entities;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MutantDetector.Infraestructure.Data
{
    public partial class mutantdnaContext : DbContext
    {
        public mutantdnaContext()
        {
        }

        public mutantdnaContext(DbContextOptions<mutantdnaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dna> Dna { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dna>(entity =>
            {
                entity.ToTable("dna");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cadena)
                    .HasColumnName("cadena")
                    .IsUnicode(false);

                entity.Property(e => e.EsMutante).HasColumnName("esMutante");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
