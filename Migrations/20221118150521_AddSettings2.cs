using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace emailapi.Migrations
{
    /// <inheritdoc />
    public partial class AddSettings2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Setting.JwtAudience");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Setting.JwtIssuer");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Setting.JwtLifetime");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Setting.JwtSecret");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Setting.OtpInvalidAllowTime");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Setting.OtpLength");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Setting.OtpLifetime");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Setting.OtpRefCodeLength");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Setting.OtpSuccessCode");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Setting.RefreshTokenLifetime");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Setting.StmpHost");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Setting.StmpPort");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Setting.StmpSecrectKey");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Setting.StmpUser");

            migrationBuilder.InsertData(
                schema: "email",
                table: "Setting",
                columns: new[] { "Key", "Value" },
                values: new object[,]
                {
                    { "Settings.JwtAudience", "https://trustmacus.com/" },
                    { "Settings.JwtIssuer", "https://trustmacus.com/" },
                    { "Settings.JwtLifetime", "5" },
                    { "Settings.JwtSecret", "5DCF9654C265776ACE7E91DF91D42" },
                    { "Settings.OtpInvalidAllowTime", "3" },
                    { "Settings.OtpLength", "5" },
                    { "Settings.OtpLifetime", "8" },
                    { "Settings.OtpRefCodeLength", "15" },
                    { "Settings.OtpSuccessCode", "99" },
                    { "Settings.RefreshTokenLifetime", "8" },
                    { "Settings.StmpHost", "smtp.gmail.com" },
                    { "Settings.StmpPort", "587" },
                    { "Settings.StmpSecrectKey", "xuedchehtcopmzqb" },
                    { "Settings.StmpUser", "pichayeanyensiri.work@gmail.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Settings.JwtAudience");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Settings.JwtIssuer");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Settings.JwtLifetime");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Settings.JwtSecret");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Settings.OtpInvalidAllowTime");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Settings.OtpLength");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Settings.OtpLifetime");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Settings.OtpRefCodeLength");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Settings.OtpSuccessCode");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Settings.RefreshTokenLifetime");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Settings.StmpHost");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Settings.StmpPort");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Settings.StmpSecrectKey");

            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Settings.StmpUser");

            migrationBuilder.InsertData(
                schema: "email",
                table: "Setting",
                columns: new[] { "Key", "Value" },
                values: new object[,]
                {
                    { "Setting.JwtAudience", "https://trustmacus.com/" },
                    { "Setting.JwtIssuer", "https://trustmacus.com/" },
                    { "Setting.JwtLifetime", "5" },
                    { "Setting.JwtSecret", "5DCF9654C265776ACE7E91DF91D42" },
                    { "Setting.OtpInvalidAllowTime", "3" },
                    { "Setting.OtpLength", "5" },
                    { "Setting.OtpLifetime", "8" },
                    { "Setting.OtpRefCodeLength", "15" },
                    { "Setting.OtpSuccessCode", "99" },
                    { "Setting.RefreshTokenLifetime", "8" },
                    { "Setting.StmpHost", "smtp.gmail.com" },
                    { "Setting.StmpPort", "587" },
                    { "Setting.StmpSecrectKey", "xuedchehtcopmzqb" },
                    { "Setting.StmpUser", "pichayeanyensiri.work@gmail.com" }
                });
        }
    }
}
