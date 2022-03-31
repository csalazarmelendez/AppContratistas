using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class DocumentosAdicionales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CertificadoMedicoLaboral",
                table: "Documento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Planilla",
                table: "Documento",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeguridadSocial",
                table: "Documento",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificadoMedicoLaboral",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "Planilla",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "SeguridadSocial",
                table: "Documento");
        }
    }
}
