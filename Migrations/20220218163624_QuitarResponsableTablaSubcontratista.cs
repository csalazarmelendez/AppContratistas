using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class QuitarResponsableTablaSubcontratista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subcontratista_Administrador_Responsable",
                table: "Subcontratista");

            migrationBuilder.DropIndex(
                name: "IX_Subcontratista_Responsable",
                table: "Subcontratista");

            migrationBuilder.DropColumn(
                name: "Responsable",
                table: "Subcontratista");

            migrationBuilder.DropColumn(
                name: "ins_mod_elim",
                table: "Subcontratista");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Responsable",
                table: "Subcontratista",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ins_mod_elim",
                table: "Subcontratista",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Subcontratista_Responsable",
                table: "Subcontratista",
                column: "Responsable");

            migrationBuilder.AddForeignKey(
                name: "FK_Subcontratista_Administrador_Responsable",
                table: "Subcontratista",
                column: "Responsable",
                principalTable: "Administrador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
