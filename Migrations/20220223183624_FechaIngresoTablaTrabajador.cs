using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class FechaIngresoTablaTrabajador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trabajador_Subcontratista_IdSubcontratista",
                table: "Trabajador");

            migrationBuilder.AlterColumn<int>(
                name: "IdSubcontratista",
                table: "Trabajador",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaIngresoObra",
                table: "Trabajador",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Trabajador_Subcontratista_IdSubcontratista",
                table: "Trabajador",
                column: "IdSubcontratista",
                principalTable: "Subcontratista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trabajador_Subcontratista_IdSubcontratista",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "FechaIngresoObra",
                table: "Trabajador");

            migrationBuilder.AlterColumn<int>(
                name: "IdSubcontratista",
                table: "Trabajador",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trabajador_Subcontratista_IdSubcontratista",
                table: "Trabajador",
                column: "IdSubcontratista",
                principalTable: "Subcontratista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
