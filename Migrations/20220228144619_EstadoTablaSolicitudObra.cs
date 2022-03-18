using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class EstadoTablaSolicitudObra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "SolicitudObra",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "SolicitudObra");
        }
    }
}
