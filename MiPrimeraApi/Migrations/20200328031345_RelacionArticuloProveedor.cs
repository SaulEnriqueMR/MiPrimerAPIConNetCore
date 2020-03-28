using Microsoft.EntityFrameworkCore.Migrations;

namespace MiPrimeraApi.Migrations
{
    public partial class RelacionArticuloProveedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProveedorID",
                table: "Articulos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_ProveedorID",
                table: "Articulos",
                column: "ProveedorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Proveedores_ProveedorID",
                table: "Articulos",
                column: "ProveedorID",
                principalTable: "Proveedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Proveedores_ProveedorID",
                table: "Articulos");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_ProveedorID",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "ProveedorID",
                table: "Articulos");
        }
    }
}
