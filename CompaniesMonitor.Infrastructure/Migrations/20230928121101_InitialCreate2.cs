using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompaniesMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                column: "NormalizedName",
                value: "User");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1345c936-7ebf-45c5-88af-f0fcdc57e919", "AQAAAAIAAYagAAAAEJVxpGmnmSXRkgKVVKvRX/IFCBdgaEo+ved5fO0Z1YMhu9IkS3uHW9t2/W1qYnSHbQ==", "6cca17ae-7e40-4d86-ac41-c1395c4be075" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330",
                column: "NormalizedName",
                value: "Human Resource");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "98a92d8e-51b7-4fd4-a73e-4e2e9ca51b34", "AQAAAAIAAYagAAAAEBWlgwFExXEnUnP3scGEKf/7yB9+4AgPQ22jHtG/X1rcYbPw/LhU6okV4AKn/TqqwA==", "e3e16629-3062-4cc6-bd46-755f644daac0" });
        }
    }
}
