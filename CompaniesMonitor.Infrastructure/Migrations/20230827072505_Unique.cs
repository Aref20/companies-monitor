using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompaniesMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EnglishName",
                table: "Partners",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Documents",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CompaniesType",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EnglishName",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_EnglishName",
                table: "Partners",
                column: "EnglishName",
                unique: true)
                .Annotation("SqlServer:Include", new[] { "ArabicName" });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Name",
                table: "Documents",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompaniesType_Name",
                table: "CompaniesType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_EnglishName",
                table: "Companies",
                column: "EnglishName",
                unique: true)
                .Annotation("SqlServer:Include", new[] { "ArabicName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Partners_EnglishName",
                table: "Partners");

            migrationBuilder.DropIndex(
                name: "IX_Documents_Name",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_CompaniesType_Name",
                table: "CompaniesType");

            migrationBuilder.DropIndex(
                name: "IX_Companies_EnglishName",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "EnglishName",
                table: "Partners",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CompaniesType",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "EnglishName",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
