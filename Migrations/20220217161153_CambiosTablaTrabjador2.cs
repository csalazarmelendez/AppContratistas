using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class CambiosTablaTrabjador2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado_ingreso",
                table: "Trabajador");

            migrationBuilder.AddColumn<string>(
                name: "EstadoIngreso",
                table: "Trabajador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoIngreso",
                table: "Trabajador");

            migrationBuilder.AddColumn<string>(
                name: "Estado_ingreso",
                table: "Trabajador",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }
    }
}
