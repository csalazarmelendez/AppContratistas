using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class ModificacionTrabajadosDocumentosNuevosValidos_FechaFinObra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CertificadoMedicoLaboralValido",
                table: "Trabajador",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FechaFinObra",
                table: "Trabajador",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FechaNacimiento",
                table: "Trabajador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlanillaValida",
                table: "Trabajador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeguridadSocialValida",
                table: "Trabajador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificadoMedicoLaboralValido",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "FechaFinObra",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "PlanillaValida",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "SeguridadSocialValida",
                table: "Trabajador");
        }
    }
}
