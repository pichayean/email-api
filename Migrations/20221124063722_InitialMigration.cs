using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace emailapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "email");

            migrationBuilder.CreateTable(
                name: "BlackList",
                schema: "email",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Otp",
                schema: "email",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    ReferenceCode = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true),
                    Expired = table.Column<DateTime>(type: "timestamp", nullable: false),
                    InvalidCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "email",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", unicode: false, nullable: false),
                    ReferenceCode = table.Column<string>(type: "text", unicode: false, nullable: false),
                    Expired = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SendEmailHistory",
                schema: "email",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", unicode: false, nullable: false),
                    Request = table.Column<string>(type: "text", maxLength: 2147483647, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendEmailHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                schema: "email",
                columns: table => new
                {
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Key);
                });

            migrationBuilder.InsertData(
                schema: "email",
                table: "Setting",
                columns: new[] { "Key", "Id", "Value" },
                values: new object[,]
                {
                    { "Settings.JwtAudience", new Guid("00000000-0000-0000-0000-000000000000"), "https://trustmacus.com/" },
                    { "Settings.JwtIssuer", new Guid("00000000-0000-0000-0000-000000000000"), "https://trustmacus.com/" },
                    { "Settings.JwtLifetime", new Guid("00000000-0000-0000-0000-000000000000"), "5" },
                    { "Settings.JwtSecret", new Guid("00000000-0000-0000-0000-000000000000"), "5DCF9654C265776ACE7E91DF91D42" },
                    { "Settings.OtpInvalidAllowTime", new Guid("00000000-0000-0000-0000-000000000000"), "3" },
                    { "Settings.OtpLength", new Guid("00000000-0000-0000-0000-000000000000"), "5" },
                    { "Settings.OtpLifetime", new Guid("00000000-0000-0000-0000-000000000000"), "8" },
                    { "Settings.OtpRefCodeLength", new Guid("00000000-0000-0000-0000-000000000000"), "15" },
                    { "Settings.OtpSuccessCode", new Guid("00000000-0000-0000-0000-000000000000"), "99" },
                    { "Settings.RefreshTokenLifetime", new Guid("00000000-0000-0000-0000-000000000000"), "8" },
                    { "Settings.StmpHost", new Guid("00000000-0000-0000-0000-000000000000"), "smtp.gmail.com" },
                    { "Settings.StmpPort", new Guid("00000000-0000-0000-0000-000000000000"), "587" },
                    { "Settings.StmpSecrectKey", new Guid("00000000-0000-0000-0000-000000000000"), "xuedchehtcopmzqb" },
                    { "Settings.StmpUser", new Guid("00000000-0000-0000-0000-000000000000"), "pichayeanyensiri.work@gmail.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackList",
                schema: "email");

            migrationBuilder.DropTable(
                name: "Otp",
                schema: "email");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "email");

            migrationBuilder.DropTable(
                name: "SendEmailHistory",
                schema: "email");

            migrationBuilder.DropTable(
                name: "Setting",
                schema: "email");
        }
    }
}
