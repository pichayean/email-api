using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace emailapi.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogCreatedTimestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReferenceCode",
                schema: "email",
                table: "Otp",
                type: "character varying(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldUnicode: false,
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "email",
                table: "Otp",
                type: "character varying(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(5)",
                oldUnicode: false,
                oldMaxLength: 5,
                oldNullable: true);

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
                    { "Setting.StmpHost", "smtp.gmail.com" },
                    { "Setting.StmpPort", "587" },
                    { "Setting.StmpSecrectKey", "xuedchehtcopmzqb" },
                    { "Setting.StmpUser", "pichayeanyensiri.work@gmail.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceCode",
                schema: "email",
                table: "Otp",
                type: "character varying(15)",
                unicode: false,
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "email",
                table: "Otp",
                type: "character varying(5)",
                unicode: false,
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
