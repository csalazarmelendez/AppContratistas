using Microsoft.EntityFrameworkCore.Migrations;

namespace Contratistas.Migrations
{
    public partial class obrCodigoTablaObras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "obrCodigo",
                table: "Obra",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "obrCodigo",
                table: "Obra");
        }
    }
}
