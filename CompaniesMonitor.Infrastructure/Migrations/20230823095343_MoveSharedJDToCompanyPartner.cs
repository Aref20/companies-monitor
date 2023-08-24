using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompaniesMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveSharedJDToCompanyPartner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "SharedJD",
                table: "Partners");

            migrationBuilder.AddColumn<double>(
                name: "Percentage",
                table: "CompaniesPartner",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SharedJD",
                table: "CompaniesPartner",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "CompaniesPartner");

            migrationBuilder.DropColumn(
                name: "SharedJD",
                table: "CompaniesPartner");

            migrationBuilder.AddColumn<double>(
                name: "Percentage",
                table: "Partners",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SharedJD",
                table: "Partners",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
