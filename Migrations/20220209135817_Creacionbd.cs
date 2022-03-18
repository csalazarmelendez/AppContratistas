using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class Creacionbd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subcontratista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIT = table.Column<int>(type: "int", maxLength: 30, nullable: false),
                    Razon_social = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    ins_mod_elim = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Responsable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcontratista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcontratista_Administrador_Responsable",
                        column: x => x.Responsable,
                        principalTable: "Administrador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudRegistro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIT = table.Column<int>(type: "int", maxLength: 30, nullable: false),
                    Razon_social = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Transaccion = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IdOperador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudRegistro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudRegistro_Operador_IdOperador",
                        column: x => x.IdOperador,
                        principalTable: "Operador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contratista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIT = table.Column<int>(type: "int", maxLength: 30, nullable: false),
                    Razon_social = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ins_mod_elim = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Responsable = table.Column<int>(type: "int", nullable: false),
                    IdOperador = table.Column<int>(type: "int", nullable: false),
                    IdSolicitudRegistro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contratista_Administrador_Responsable",
                        column: x => x.Responsable,
                        principalTable: "Administrador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contratista_Operador_IdOperador",
                        column: x => x.IdOperador,
                        principalTable: "Operador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contratista_SolicitudRegistro_IdSolicitudRegistro",
                        column: x => x.IdSolicitudRegistro,
                        principalTable: "SolicitudRegistro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Trabajador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado_ingreso = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    NIT_Contratista = table.Column<int>(type: "int", nullable: false),
                    NIT_Subcontratista = table.Column<int>(type: "int", nullable: true),
                    ins_mod_elim = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Responsable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trabajador_Administrador_Responsable",
                        column: x => x.Responsable,
                        principalTable: "Administrador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trabajador_Contratista_NIT_Contratista",
                        column: x => x.NIT_Contratista,
                        principalTable: "Contratista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Trabajador_Subcontratista_NIT_Subcontratista",
                        column: x => x.NIT_Subcontratista,
                        principalTable: "Subcontratista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<byte[]>(type: "image", nullable: false),
                    EPS = table.Column<byte[]>(type: "image", nullable: false),
                    ARL = table.Column<byte[]>(type: "image", nullable: false),
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
                name: "IX_Contratista_IdOperador",
                table: "Contratista",
                column: "IdOperador");

            migrationBuilder.CreateIndex(
                name: "IX_Contratista_IdSolicitudRegistro",
                table: "Contratista",
                column: "IdSolicitudRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_Contratista_Responsable",
                table: "Contratista",
                column: "Responsable");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_Trabajador",
                table: "Documento",
                column: "Trabajador");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudRegistro_IdOperador",
                table: "SolicitudRegistro",
                column: "IdOperador");

            migrationBuilder.CreateIndex(
                name: "IX_Subcontratista_Responsable",
                table: "Subcontratista",
                column: "Responsable");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajador_NIT_Contratista",
                table: "Trabajador",
                column: "NIT_Contratista");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajador_NIT_Subcontratista",
                table: "Trabajador",
                column: "NIT_Subcontratista");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajador_Responsable",
                table: "Trabajador",
                column: "Responsable");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documento");

            migrationBuilder.DropTable(
                name: "Trabajador");

            migrationBuilder.DropTable(
                name: "Contratista");

            migrationBuilder.DropTable(
                name: "Subcontratista");

            migrationBuilder.DropTable(
                name: "SolicitudRegistro");

            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropTable(
                name: "Operador");
        }
    }
}
