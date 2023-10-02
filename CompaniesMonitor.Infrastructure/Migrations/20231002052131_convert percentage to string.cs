using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompaniesMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class convertpercentagetostring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Percentage",
                table: "CompaniesPartner",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b61fd89-8492-4dc5-9e56-a94accac5b52", "AQAAAAIAAYagAAAAEIT3p2GGThiFh1jWVGRGS/v+Ym7ve0sK5e0wb8ufFig7XOdXCFGVDTrNCz+m36LjyA==", "dd31b473-c5bd-4263-a9a7-f8d52e638000" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Percentage",
                table: "CompaniesPartner",
                type: "float",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1345c936-7ebf-45c5-88af-f0fcdc57e919", "AQAAAAIAAYagAAAAEJVxpGmnmSXRkgKVVKvRX/IFCBdgaEo+ved5fO0Z1YMhu9IkS3uHW9t2/W1qYnSHbQ==", "6cca17ae-7e40-4d86-ac41-c1395c4be075" });
        }
    }
}
