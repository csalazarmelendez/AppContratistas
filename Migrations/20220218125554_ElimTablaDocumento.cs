using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class ElimTablaDocumento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ARL = table.Column<byte[]>(type: "image", nullable: false),
                    Cedula = table.Column<byte[]>(type: "image", nullable: false),
                    EPS = table.Column<byte[]>(type: "image", nullable: false),
                    Pension = table.Column<byte[]>(type: "image", nullable: false),
                    Trabajador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documento_Trabajador_Trabajador",
                        column: x => x.Trabajador,
                        principalTable: "Trabajador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documento_Trabajador",
                table: "Documento",
                column: "Trabajador");
        }
    }
}
