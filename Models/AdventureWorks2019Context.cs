using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OpenApiProject1.Models;

public partial class AdventureWorks2019Context : DbContext
{
    public AdventureWorks2019Context()
    {
    }

    public AdventureWorks2019Context(DbContextOptions<AdventureWorks2019Context> options)
        : base(options)
    {
    }

    public virtual DbSet<LoanDetail> LoanDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=10.1.15.40;Database=AdventureWorks2019;User Id=Traininguser;Password=Traininguser;Trusted_Connection=False;TrustServerCertificate=True;;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoanDetail>(entity =>
        {
            entity.HasKey(e => e.Lid).HasName("PK__LoanDeta__C6555721F6C59708");

            entity.ToTable("LoanDetail");

            entity.Property(e => e.Lid).HasColumnName("LID");
            entity.Property(e => e.LoanNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
