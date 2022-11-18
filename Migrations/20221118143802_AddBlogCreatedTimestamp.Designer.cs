﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using email_api.Database;

#nullable disable

namespace emailapi.Migrations
{
    [DbContext(typeof(EmailContext))]
    [Migration("20221118143802_AddBlogCreatedTimestamp")]
    partial class AddBlogCreatedTimestamp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("email")
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("email_api.Database.AuthenticationHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Expired")
                        .HasColumnType("timestamp");

                    b.Property<string>("ReferenceCode")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AuthenticationHistory", "email");
                });

            modelBuilder.Entity("email_api.Database.BlackList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("BlackList", "email");
                });

            modelBuilder.Entity("email_api.Database.Otp", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("Expired")
                        .HasColumnType("timestamp");

                    b.Property<int>("InvalidCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<string>("ReferenceCode")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Otp", "email");
                });

            modelBuilder.Entity("email_api.Database.SendEmailHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp");

                    b.Property<string>("Request")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .IsUnicode(true)
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SendEmailHistory", "email");
                });

            modelBuilder.Entity("email_api.Database.Setting", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Key");

                    b.ToTable("Setting", "email");

                    b.HasData(
                        new
                        {
                            Key = "Setting.JwtIssuer",
                            Value = "https://trustmacus.com/"
                        },
                        new
                        {
                            Key = "Setting.JwtAudience",
                            Value = "https://trustmacus.com/"
                        },
                        new
                        {
                            Key = "Setting.JwtLifetime",
                            Value = "5"
                        },
                        new
                        {
                            Key = "Setting.JwtSecret",
                            Value = "5DCF9654C265776ACE7E91DF91D42"
                        },
                        new
                        {
                            Key = "Setting.StmpSecrectKey",
                            Value = "xuedchehtcopmzqb"
                        },
                        new
                        {
                            Key = "Setting.StmpHost",
                            Value = "smtp.gmail.com"
                        },
                        new
                        {
                            Key = "Setting.StmpPort",
                            Value = "587"
                        },
                        new
                        {
                            Key = "Setting.StmpUser",
                            Value = "pichayeanyensiri.work@gmail.com"
                        },
                        new
                        {
                            Key = "Setting.OtpLength",
                            Value = "5"
                        },
                        new
                        {
                            Key = "Setting.OtpRefCodeLength",
                            Value = "15"
                        },
                        new
                        {
                            Key = "Setting.OtpLifetime",
                            Value = "8"
                        },
                        new
                        {
                            Key = "Setting.OtpInvalidAllowTime",
                            Value = "3"
                        },
                        new
                        {
                            Key = "Setting.OtpSuccessCode",
                            Value = "99"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
