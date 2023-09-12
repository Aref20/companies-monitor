using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompaniesMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addnewfileds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Partners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NationalID",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a50f2fea-e9c1-4dbe-9fa8-d9f8fdb16ff9", "c75a707d-162f-4276-a86c-6040947c548f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "NationalID",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Companies");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2c363551-329c-4817-a9d7-5d93b1dc6510", "afe0e288-09b2-46c7-94ac-9ebc795c60b6" });
        }
    }
}
