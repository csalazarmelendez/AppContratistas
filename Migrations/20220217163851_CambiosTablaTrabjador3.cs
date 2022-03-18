using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class CambiosTablaTrabjador3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trabajador_Contratista_NIT_Contratista",
                table: "Trabajador");

            migrationBuilder.DropForeignKey(
                name: "FK_Trabajador_Subcontratista_NIT_Subcontratista",
                table: "Trabajador");

            migrationBuilder.RenameColumn(
                name: "NIT_Subcontratista",
                table: "Trabajador",
                newName: "IdSubcontratista");

            migrationBuilder.RenameColumn(
                name: "NIT_Contratista",
                table: "Trabajador",
                newName: "IdContratista");

            migrationBuilder.RenameIndex(
                name: "IX_Trabajador_NIT_Subcontratista",
                table: "Trabajador",
                newName: "IX_Trabajador_IdSubcontratista");

            migrationBuilder.RenameIndex(
                name: "IX_Trabajador_NIT_Contratista",
                table: "Trabajador",
                newName: "IX_Trabajador_IdContratista");

            migrationBuilder.AddForeignKey(
                name: "FK_Trabajador_Contratista_IdContratista",
                table: "Trabajador",
                column: "IdContratista",
                principalTable: "Contratista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trabajador_Subcontratista_IdSubcontratista",
                table: "Trabajador",
                column: "IdSubcontratista",
                principalTable: "Subcontratista",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trabajador_Contratista_IdContratista",
                table: "Trabajador");

            migrationBuilder.DropForeignKey(
                name: "FK_Trabajador_Subcontratista_IdSubcontratista",
                table: "Trabajador");

            migrationBuilder.RenameColumn(
                name: "IdSubcontratista",
                table: "Trabajador",
                newName: "NIT_Subcontratista");

            migrationBuilder.RenameColumn(
                name: "IdContratista",
                table: "Trabajador",
                newName: "NIT_Contratista");

            migrationBuilder.RenameIndex(
                name: "IX_Trabajador_IdSubcontratista",
                table: "Trabajador",
                newName: "IX_Trabajador_NIT_Subcontratista");

            migrationBuilder.RenameIndex(
                name: "IX_Trabajador_IdContratista",
                table: "Trabajador",
                newName: "IX_Trabajador_NIT_Contratista");

            migrationBuilder.AddForeignKey(
                name: "FK_Trabajador_Contratista_NIT_Contratista",
                table: "Trabajador",
                column: "NIT_Contratista",
                principalTable: "Contratista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trabajador_Subcontratista_NIT_Subcontratista",
                table: "Trabajador",
                column: "NIT_Subcontratista",
                principalTable: "Subcontratista",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
