using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Unidad3P1.Data.Migrations
{
    /// <inheritdoc />
    public partial class Restaurante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarritoCompra",
                columns: table => new
                {
                    CarritoCompraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoCompra", x => x.CarritoCompraId);
                    table.ForeignKey(
                        name: "FK_CarritoCompra_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId");
                });

            migrationBuilder.CreateTable(
                name: "CarritoCompraItem",
                columns: table => new
                {
                    CarritoCompraItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    CarritoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoCompraItem", x => x.CarritoCompraItemId);
                    table.ForeignKey(
                        name: "FK_CarritoCompraItem_CarritoCompra_CarritoId",
                        column: x => x.CarritoId,
                        principalTable: "CarritoCompra",
                        principalColumn: "CarritoCompraId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarritoCompraItem_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarritoCompra_ProductoId",
                table: "CarritoCompra",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoCompraItem_CarritoId",
                table: "CarritoCompraItem",
                column: "CarritoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoCompraItem_ProductoId",
                table: "CarritoCompraItem",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritoCompraItem");

            migrationBuilder.DropTable(
                name: "CarritoCompra");
        }
    }
}
