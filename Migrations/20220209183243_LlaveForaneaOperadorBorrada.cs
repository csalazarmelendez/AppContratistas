using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class LlaveForaneaOperadorBorrada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudRegistro_Operador_IdOperador",
                table: "SolicitudRegistro");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudRegistro_IdOperador",
                table: "SolicitudRegistro");

            migrationBuilder.RenameColumn(
                name: "IdOperador",
                table: "SolicitudRegistro",
                newName: "NumeroOperador");

            migrationBuilder.AddColumn<string>(
                name: "Operador",
                table: "SolicitudRegistro",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Operador",
                table: "SolicitudRegistro");

            migrationBuilder.RenameColumn(
                name: "NumeroOperador",
                table: "SolicitudRegistro",
                newName: "IdOperador");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudRegistro_IdOperador",
                table: "SolicitudRegistro",
                column: "IdOperador");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudRegistro_Operador_IdOperador",
                table: "SolicitudRegistro",
                column: "IdOperador",
                principalTable: "Operador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
