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

    public virtual DbSet<RefreshTokenEntity> RefreshToken { get; set; }
    public virtual DbSet<SendEmailHistoryEntity> SendEmailHistory { get; set; }
    public virtual DbSet<OtpEntity> Otp { get; set; }
    public virtual DbSet<BlackListEntity> BlackList { get; set; }
    public virtual DbSet<SettingEntity> Setting { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:DefaultSchema", "email");

        modelBuilder.Entity<SendEmailHistoryEntity>(entity =>
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

        modelBuilder.Entity<RefreshTokenEntity>(entity =>
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

        modelBuilder.Entity<OtpEntity>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ReferenceCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Expired)
                .HasColumnType("timestamp");

            entity.Property(e => e.InvalidCount)
                .HasDefaultValue(0);
        });

        modelBuilder.Entity<BlackListEntity>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SettingEntity>(entity =>
        {
            entity.HasKey(e => e.Key);

            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .IsUnicode(false);  
        });
        modelBuilder.Entity<SettingEntity>()
        .HasData(
            new SettingEntity
            {
                Key = "Settings.RefreshTokenLifetime",
                Value = "8"
            },
            new SettingEntity
            {
                Key = "Settings.JwtIssuer",
                Value = "https://trustmacus.com/"
            },
            new SettingEntity
            {
                Key = "Settings.JwtAudience",
                Value = "https://trustmacus.com/"
            },
            new SettingEntity
            {
                Key = "Settings.JwtLifetime",
                Value = "5"
            },
            new SettingEntity
            {
                Key = "Settings.JwtSecret",
                Value = "5DCF9654C265776ACE7E91DF91D42"
            },

            new SettingEntity
            {
                Key = "Settings.StmpSecrectKey",
                Value = "xuedchehtcopmzqb"
            },
            new SettingEntity
            {
                Key = "Settings.StmpHost",
                Value = "smtp.gmail.com"
            },
            new SettingEntity
            {
                Key = "Settings.StmpPort",
                Value = "587"
            },
            new SettingEntity
            {
                Key = "Settings.StmpUser",
                Value = "pichayeanyensiri.work@gmail.com"
            },

            new SettingEntity
            {
                Key = "Settings.OtpLength",
                Value = "5"
            },
            new SettingEntity
            {
                Key = "Settings.OtpRefCodeLength",
                Value = "15"
            },
            new SettingEntity
            {
                Key = "Settings.OtpLifetime",
                Value = "8"
            },
            new SettingEntity
            {
                Key = "Settings.OtpInvalidAllowTime",
                Value = "3"
            },
            new SettingEntity
            {
                Key = "Settings.OtpSuccessCode",
                Value = "99"
            }
        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}