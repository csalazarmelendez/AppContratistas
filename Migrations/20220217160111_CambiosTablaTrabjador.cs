using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class CambiosTablaTrabjador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trabajador_Administrador_Responsable",
                table: "Trabajador");

            migrationBuilder.DropIndex(
                name: "IX_Trabajador_Responsable",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "Responsable",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "ins_mod_elim",
                table: "Trabajador");

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "Trabajador",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonaContacto",
                table: "Trabajador",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Trabajador",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelefonoContacto",
                table: "Trabajador",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "PersonaContacto",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "TelefonoContacto",
                table: "Trabajador");

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Trabajador",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Responsable",
                table: "Trabajador",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ins_mod_elim",
                table: "Trabajador",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajador_Responsable",
                table: "Trabajador",
                column: "Responsable");

            migrationBuilder.AddForeignKey(
                name: "FK_Trabajador_Administrador_Responsable",
                table: "Trabajador",
                column: "Responsable",
                principalTable: "Administrador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
