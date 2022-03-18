using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class TablaTrabajadorXObraYObservacionesTrabajadorContratista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "Trabajador",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "Contratista",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TrabajadorXObra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObrId = table.Column<int>(type: "int", nullable: false),
                    IdTrabajador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrabajadorXObra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrabajadorXObra_Obra_ObrId",
                        column: x => x.ObrId,
                        principalTable: "Obra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrabajadorXObra_Trabajador_IdTrabajador",
                        column: x => x.IdTrabajador,
                        principalTable: "Trabajador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadorXObra_IdTrabajador",
                table: "TrabajadorXObra",
                column: "IdTrabajador");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadorXObra_ObrId",
                table: "TrabajadorXObra",
                column: "ObrId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrabajadorXObra");

            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "Contratista");
        }
    }
}
