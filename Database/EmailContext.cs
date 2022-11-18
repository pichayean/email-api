using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable
namespace email_api.Database;
public partial class EmailContext : DbContext
{
    public EmailContext()
    {
    }

    public EmailContext(DbContextOptions<EmailContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthenticationHistory> AuthenticationHistory { get; set; }
    public virtual DbSet<SendEmailHistory> SendEmailHistory { get; set; }
    public virtual DbSet<Otp> Otp { get; set; }
    public virtual DbSet<BlackList> BlackList { get; set; }
    public virtual DbSet<Setting> Setting { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:DefaultSchema", "email");

        modelBuilder.Entity<SendEmailHistory>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Token)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.Request)
                .IsRequired()
                .HasMaxLength(int.MaxValue)
                .IsUnicode(true);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp");
        });

        modelBuilder.Entity<AuthenticationHistory>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Email)
                .IsRequired();

            entity.Property(e => e.ReferenceCode)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.RefreshToken)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.Expired)
                .HasColumnType("timestamp");
        });

        modelBuilder.Entity<Otp>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Code)
                .HasMaxLength(5)
                .IsUnicode(false);

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ReferenceCode)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.Property(e => e.Expired)
                .HasColumnType("timestamp");

            entity.Property(e => e.InvalidCount)
                .HasDefaultValue(0);
        });

        modelBuilder.Entity<BlackList>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.Key);

            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}