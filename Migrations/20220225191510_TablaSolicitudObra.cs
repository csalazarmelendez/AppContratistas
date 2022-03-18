using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class TablaSolicitudObra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdSolicitudObra",
                table: "TrabajadorXObra",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SolicitudObra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObrId = table.Column<int>(type: "int", nullable: false),
                    IdTrabajador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudObra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudObra_Obra_ObrId",
                        column: x => x.ObrId,
                        principalTable: "Obra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudObra_Trabajador_IdTrabajador",
                        column: x => x.IdTrabajador,
                        principalTable: "Trabajador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadorXObra_IdSolicitudObra",
                table: "TrabajadorXObra",
                column: "IdSolicitudObra");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudObra_IdTrabajador",
                table: "SolicitudObra",
                column: "IdTrabajador");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudObra_ObrId",
                table: "SolicitudObra",
                column: "ObrId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrabajadorXObra_SolicitudObra_IdSolicitudObra",
                table: "TrabajadorXObra",
                column: "IdSolicitudObra",
                principalTable: "SolicitudObra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrabajadorXObra_SolicitudObra_IdSolicitudObra",
                table: "TrabajadorXObra");

            migrationBuilder.DropTable(
                name: "SolicitudObra");

            migrationBuilder.DropIndex(
                name: "IX_TrabajadorXObra_IdSolicitudObra",
                table: "TrabajadorXObra");

            migrationBuilder.DropColumn(
                name: "IdSolicitudObra",
                table: "TrabajadorXObra");
        }
    }
}
