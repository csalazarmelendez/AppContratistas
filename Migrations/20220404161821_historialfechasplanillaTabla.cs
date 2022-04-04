using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class historialfechasplanillaTabla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistorialFechasPlanilla",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTrabajador = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialFechasPlanilla", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialFechasPlanilla_Trabajador_IdTrabajador",
                        column: x => x.IdTrabajador,
                        principalTable: "Trabajador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistorialFechasPlanilla_IdTrabajador",
                table: "HistorialFechasPlanilla",
                column: "IdTrabajador");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorialFechasPlanilla");
        }
    }
}
