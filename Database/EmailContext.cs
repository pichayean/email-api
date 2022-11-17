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

    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<AuthenticationHistory> AuthenticationHistory { get; set; }
    public virtual DbSet<SendEmailHistory> SendEmailHistory { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:DefaultSchema", "email");

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SendEmailHistory>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Token)
                .IsRequired();

            entity.Property(e => e.Request)
                .IsRequired()
                .HasMaxLength(int.MaxValue);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp");
        });


        modelBuilder.Entity<AuthenticationHistory>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Email)
                .IsRequired();

            entity.Property(e => e.SignInDate)
                .HasColumnType("timestamp");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}