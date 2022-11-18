using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                name: "AuthenticationHistory",
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
                    table.PrimaryKey("PK_AuthenticationHistory", x => x.Id);
                });

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
                    ReferenceCode = table.Column<string>(type: "character varying(15)", unicode: false, maxLength: 15, nullable: true),
                    Code = table.Column<string>(type: "character varying(5)", unicode: false, maxLength: 5, nullable: true),
                    Expired = table.Column<DateTime>(type: "timestamp", nullable: false),
                    InvalidCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otp", x => x.Id);
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
                    Value = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Key);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthenticationHistory",
                schema: "email");

            migrationBuilder.DropTable(
                name: "BlackList",
                schema: "email");

            migrationBuilder.DropTable(
                name: "Otp",
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
