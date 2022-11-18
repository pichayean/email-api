using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace emailapi.Migrations
{
    /// <inheritdoc />
    public partial class AddSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "email",
                table: "Setting",
                columns: new[] { "Key", "Value" },
                values: new object[] { "Setting.RefreshTokenLifetime", "8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "email",
                table: "Setting",
                keyColumn: "Key",
                keyValue: "Setting.RefreshTokenLifetime");
        }
    }
}
