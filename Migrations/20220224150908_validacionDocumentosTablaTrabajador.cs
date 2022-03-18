using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class validacionDocumentosTablaTrabajador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ARLValida",
                table: "Trabajador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CedulaValida",
                table: "Trabajador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EPSValida",
                table: "Trabajador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PensionValida",
                table: "Trabajador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ARLValida",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "CedulaValida",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "EPSValida",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "PensionValida",
                table: "Trabajador");
        }
    }
}
